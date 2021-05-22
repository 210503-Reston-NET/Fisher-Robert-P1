using StoreModels;
using StoreBL;
using System.Collections.Generic;

namespace StoreUI
{
    public class ManagerMenu : StoreMenu
    {
        MyValidate validate = new StringValidator();
        StoreBLInterface bussinessLayer;

        public ManagerMenu (StoreBLInterface BL, User passedUser)
        {
            this.bussinessLayer = BL;
            this.CurrentUser = passedUser;
        }
        public override void Start()
        {
            bool repeat = true;

            do {
            string output = "--------Manager Menu--------" + "\n";
            output += "Hello " + this.CurrentUser.FirstName + "!" + "\n";
            output += "[0] Search Customer." + "\n";
            output += "[1] Search Store." + "\n";
            output += "[2] Edit Product." + "\n";
            output += "[3] Exit." + "\n";

            string userInput = validate.ValidateString(output);

            switch (userInput)
            {
                // Case: View Customers
                case "0":
                    SearchCustomer();
                    break;
                case "1":
                    SearchStore();
                    break;
                case "2":
                    MenuFactory.GetMenu("EditProduct", this.CurrentUser).Start();
                    break;
                case "3":
                    repeat = false;
                    break;
                default:
                    break;
            }
            } while(repeat);
        }

        public string SearchCustomer()
        {
            List<User> users = bussinessLayer.GetAllUsers();
            int index = 0;

            foreach (User account in users)
                System.Console.WriteLine("[" + index++ + "] " + account.UserName);
            
            int userInput = validate.ValidateInteger("");

            List<Order> orders = bussinessLayer.GetAllOrders(users[userInput]);

            foreach(Order order in orders){
                order.Transactions = bussinessLayer.GetTransactions(order.OrderNumber);
            }

            foreach(Order order in orders){
                System.Console.WriteLine("--------Order: " + order.OrderNumber + "--------");
                System.Console.WriteLine(order);
                System.Console.WriteLine("\t----Transactions----");
                foreach(Transaction transact in order.Transactions)
                {
                    System.Console.WriteLine("\t" + transact);
                }
            }
            return "";
        }

        public string SearchStore()
        {
            List<Store> stores= bussinessLayer.GetAllStores();
            int index = 0;

            foreach(Store store in stores)
                System.Console.WriteLine("[" + index++ + "] BearlyCamping in " + store.StoreCity + ", " + store.StoreState);


            int userInput = validate.ValidateInteger("");
            Store selectedStore = stores[userInput];

            string output = "--------Store Details <" + selectedStore.StoreCity + ", " + selectedStore.StoreState + "--------\n";
            output += "[0] Get Related Orders.\n";
            output += "[1] Get Inventory for this location.\n";
            output += "[2] Replenish Inventory.";

            userInput = validate.ValidateInteger(output);
            Product relatedProduct;

            switch(userInput)
            {
                case 0:
                List<Order> orders = bussinessLayer.GetAllOrders(selectedStore.StoreID);
                foreach (Order order in orders){
                    System.Console.WriteLine("Order#: " + order.OrderNumber + "\tCustomer: " + order.UserName + "\tTotal: $" + order.Total);
                    foreach (Transaction transact in order.Transactions){
                        relatedProduct = bussinessLayer.GetProduct(transact.ISBN);
                        System.Console.WriteLine("\tProduct: " + relatedProduct.Name + "\tPrice: " + relatedProduct.Price +
                        "\tQuantity: " + transact.Quantity);
                    }
                    System.Console.WriteLine();
                }
                break;
                
                case 1:
                List<Inventory> inventories = bussinessLayer.GetInventoryFor(selectedStore.StoreID);

                foreach(Inventory inventory in inventories){
                    relatedProduct = bussinessLayer.GetProduct(inventory.ISBN);
                    System.Console.WriteLine("Product#: " + inventory.ISBN + "\tProductName: " + relatedProduct.Name +
                    "\tCost: " + relatedProduct.Price + "\tQuantity: " + inventory.Quantity);
                }
                break;

                case 2:
                List<Inventory> inventorie = bussinessLayer.GetInventoryFor(selectedStore.StoreID);

                foreach(Inventory inventory in inventorie){
                    relatedProduct = bussinessLayer.GetProduct(inventory.ISBN);
                    System.Console.WriteLine("Product#: " + inventory.ISBN + "\tProductName: " + relatedProduct.Name +
                    "\tCost: " + relatedProduct.Price + "\tQuantity: " + inventory.Quantity);

                    output = "Enter the new Quantity for the transaction";
                    userInput = validate.ValidateInteger(output);

                    inventory.Quantity = userInput;

                    bussinessLayer.UpdateInventory(inventory);
                }
                break;
                default:
                break;
            
            }
            
            return "";
        }
    }
}