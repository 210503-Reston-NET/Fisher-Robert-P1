using StoreModels;
using StoreBL;
using StoreDL;
using System.Collections.Generic;
using System;
using Serilog;


namespace StoreUI
{
    public class OrderMenu : StoreMenu
    {
        StoreBLInterface bussinessLayer;
        MyValidate validate = new StringValidator();
        public OrderMenu(StoreBLInterface BL, User PassedUser)
        {
            bussinessLayer = BL;
            CurrentUser = PassedUser;

            // Initialize Serilogger
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("../logs/StoreApp.txt", rollingInterval : RollingInterval.Day)
            .CreateLogger();
        }
        public override void Start()
        {
            bool repeat = true;
            decimal total = 0.00M;
            string city;
            string state;
            List<Inventory> StoreStock;
            Store FoundStore = null;
            Store TargetStore = null;
            List<Transaction> OrderTransactions = new List<Transaction>();
            List<Product> Products = new List<Product>();

            // Intro into Order Menu
            string output = "--------Order Menu--------";
                output += "\n Welcome to the Order Menu!\n";
                System.Console.WriteLine(output);

            // Get Store Infor from User
            while (FoundStore == null){  
                output = "Please insert the City from where your purchasing.";
                city = validate.ValidateString(output);

                output = "Please insert the State from where your purchasing.";
                state = validate.ValidateString(output);

                TargetStore = new Store(city, state);
                FoundStore = bussinessLayer.GetStore(TargetStore);

                if (FoundStore == null)
                    System.Console.WriteLine("Im sorry, that is an invalid store, Please try again!");
            }
            
            // Loop until All purchases are made
            do
            {
                int index = 0;

            // Populate options for User to purchase. 
            // Allows User to keep selecting options until they are ready to check out or
            // Inventory runs out.
            System.Console.WriteLine("------------OrderMenu Page------------");
            System.Console.WriteLine("Add whatever items you like!");

            StoreStock = bussinessLayer.GetInventory(FoundStore.StoreID);

            // Create List of products for user
            foreach (Inventory inventory in StoreStock)
            {
                Product item = bussinessLayer.GetProduct(inventory.ISBN);
                Products.Add(item);
                System.Console.WriteLine("[" + index++ + "] : " + item.ToString() + "\tQTY: " + inventory.Quantity);
            }

            // Finally Create escape case
            System.Console.WriteLine("[" + index + "] Exit");
            // Get user input, store data into a list of transactions for Order
            try
            {
                output = "Select from one of the items above by inserting the related number.";
                int selector = validate.ValidateInteger(output);

                if (selector != index){
                    bool found = false;
                    // Instantiate a new product
                    Product item = new Product();
                    Product selected = Products[selector];

                    item.Name = selected.Name;
                    item.ISBN = selected.ISBN;
                    item.Price = selected.Price;

                    // Make sure the product does not already exist in order and that there is enough in quantity
                    foreach (Transaction transact in OrderTransactions)
                    {
                        Inventory storeInventory = bussinessLayer.GetInventory(FoundStore.StoreID, transact.ISBN);
            
                        // Checks to see if the item exists in the 
                        if (transact.ISBN == item.ISBN){
                            // Checks to see if there is enough in stock
                            if (transact.Quantity == storeInventory.Quantity)
                                System.Console.WriteLine("There is not enough in stock to purchase this");
                            else
                                transact.Quantity++;
                            found = true;
                        }
                    }
                    // If not found, then add to list
                    if (!found)
                        OrderTransactions.Add(new Transaction(item.ISBN, 1));

                    // Adds the price of the item to the total for the order
                    total += item.Price;
                }

                // Case: Escape
                if (selector == index){
                    repeat = false;
                    System.Console.WriteLine("--------Your Order--------");

                    // Set a new order for the given location
                    Order order= new Order(FoundStore.StoreID, this.CurrentUser.UserName, total){
                        StoreID = FoundStore.StoreID,
                        UserName = this.CurrentUser.UserName,
                        Total = total
                    };
                    order.Create = DateTime.Now;
                    
                    // Attempt to AddOrder to orders and transactions to ttransactions and related Inventories to the given transactions
                    try {
                    Log.Debug("Adding Order, Transactions, and Inventories to DB");
                    order = bussinessLayer.AddOrder(order);

                    Order neworder = bussinessLayer.GetOrder(order);

                    neworder.Transactions = OrderTransactions;

                    // Add each transaction individually as well as associated Inventory
                    foreach (Transaction transact in neworder.Transactions){
                        transact.OrderNumber = neworder.OrderNumber;
                        bussinessLayer.AddTransaction(transact);

                        Inventory relatedInventory = new Inventory(){
                            ISBN = transact.ISBN,
                            StoreID = FoundStore.StoreID,
                            Quantity = bussinessLayer.GetInventory(FoundStore.StoreID, transact.ISBN).Quantity - transact.Quantity
                        };

                        bussinessLayer.UpdateInventory(relatedInventory);
                    }

                    } catch (Exception e)
                    {
                        Log.Error(e.Message, "Failed to add Order/Transactions/Inventory to DB.");
                    }

                    foreach (Transaction transact in OrderTransactions)
                        System.Console.WriteLine(bussinessLayer.GetProduct(transact.ISBN) + "\tQTY: " + transact);

                    System.Console.WriteLine("\nTotal: " + total);
                    System.Console.WriteLine();
                }

            }
            catch(Exception e)
            {
                Log.Error(e.Message, "Entered Invalid index in OrderMenu.");
                System.Console.WriteLine("Please insert a number between 0 and " + index);
            }
                
        } while (repeat);
        }
    }
}