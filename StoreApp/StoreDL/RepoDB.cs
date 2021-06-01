using System.Collections.Generic;
using Models = StoreModels;
using System.Linq;
using System;
using System.Data;
using StoreModels;
using Serilog;

namespace StoreDL
{
    public class RepoDB : DAO
    {
        public BearlyCampingDataContext _context;
        public RepoDB(BearlyCampingDataContext context)
        {
            _context = context;

            // Initialize Serilogger
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("../logs/StoreApp.txt", rollingInterval : RollingInterval.Infinite)
            .CreateLogger();
        }

        public Models.Order AddOrder(Order order)
        {
            try
            {
                Log.Debug("Attempting to Persist order: {} to Database.");
                _context.Orders.Add(order);
                _context.SaveChanges();
                

                return order;
            } catch(Exception e)
            {
                Log.Error(e.Message, "Failed to add order: " + order +" to database.");
                return null;
            }
        }

        public bool AddTransaction(Transaction transact)
        {
            try {
            Log.Debug("Attempting to persist transaction: " + transact + " to database.");
            _context.Transactions.Add(
                transact
            );
            _context.SaveChanges();
            } catch(Exception e)
            {
                Log.Error(e.Message, "Failed to persist transaction: " + transact + " to database");
                return false;
            }
            return true;
        }

        public bool AddProduct(Product product)
        {
            try
            {
                Log.Debug("Attempting to persist transaction: " + product + " to database.");
                _context.Products.Add(
                    product
                );
                _context.SaveChanges();
                return true;
            } catch(Exception e)
            {
                Log.Error(e.Message, "Failed to persist transaction: " + product + " to database");
                return false;
            }
        }

        public bool AddStore(Store store)
        {
            try
            {
                Log.Debug("Attempting to persist Store: " + store + " to database.");
                _context.Stores.Add(
                    store
                );
                _context.SaveChanges();

                return true;
            } catch(Exception e)
            {
                Log.Error(e.Message, "Failed to persist Store: " + store + " as well as its inventory to database.");
                return false;
            }
        }

        public bool AddUser(User user)
        {
            try {
                Log.Debug("Attempting to persist User: " + user + " to database.");
            _context.Users.Add(
               user
            );
            _context.SaveChanges();
            return true;
            } catch (Exception e)
            {
                Log.Error(e.Message, "Failed to persist User: " + user + " to database.");
                return false;
            }
        }

        public List<Store> GetAllStores()
        {
            List<Store> models;
            try{
            Log.Debug("Attempting to retrieve Stores from the database.");
            models = _context.Stores
            .Select(
                store => store
            ).ToList();
            } catch(Exception e)
            {
                Log.Error(e.Message, "Failed to retrieve Stores from database.");
                return null;
            }
            if (models.Count > 0)
                return models;
            return new List<Store>();
        }

        public List<User> GetAllUsers()
        {
            List<User> users;
            try{
            Log.Debug("Attempting to retrieve Users from the database.");
            users = _context.Users
            .Select(
                account => new User(account.UserName, account.Password, account.FirstName, account.LastName, account.Code, account.created)
            ).ToList();
            } catch(Exception e)
            {
                Log.Error(e.Message, "Failed to retrieve Users from database.");
                return null;
            }
            return users;
        }

        public List<Inventory> getInventory(int storeID)
        {
            List<Inventory> inventory;
            try{
            Log.Debug("Attempting to retrieve Inventory from the database.");
            inventory = _context.Inventories.Where(
                store => store.StoreID == storeID
                ).Select(
                    Inventory => new Inventory()
                    {
                        ISBN = Inventory.ISBN,
                        StoreID = Inventory.StoreID,
                        Quantity = Inventory.Quantity
                    }
                ).ToList();
            } catch(Exception e)
            {
                Log.Error(e.Message, "Failed to retrieve Inventory from database.");
                return null;
            }
            return inventory;
        }

        public Inventory GetInventory(int storeID, string Isbn){
            Models.Inventory inventory;
            try{
            Log.Debug("Attempting to retrieve Inventory from the database.");
            inventory = _context.Inventories.First(invent => invent.StoreID == storeID && invent.ISBN == Isbn);
            } catch (Exception e)
            {
                Log.Error(e.Message, "Failed to retrieve Inventory from database.");
                return null;
            }
            return new Inventory()
            {
                ISBN = inventory.ISBN,
                StoreID = inventory.StoreID,
                Quantity = inventory.Quantity
            };
        }

        public Order GetOrder(Order order)
        {
            Order found;
            try {
            Log.Debug("Attempting to retrieve Inventory from the database.");
            found = _context.Orders.FirstOrDefault(DBOrder => DBOrder.OrderNumber == order.OrderNumber && DBOrder.StoreID == order.StoreID &&
            DBOrder.UserName == order.UserName);

            } catch (Exception e)
            {
                Log.Error(e.Message, "Failed to retrieve Inventory from database.");
                return null;
            }
            return new Order(){
                OrderNumber = found.OrderNumber,
                StoreID = found.StoreID,
                Total = found.Total,
                Create = found.Create,
                UserName = found.UserName
            };
        }

        public List<Order> GetOrdersFor(int storeID)
        {
            List<Order> orders;
            try{
            Log.Debug("Attempting to retrieve list of orders from the database.");
            orders = _context.Orders.Where(
                order => order.StoreID == storeID).Select(
                    order => new Order()
                    {
                        OrderNumber = order.OrderNumber,
                        UserName = order.UserName,
                        StoreID = storeID,
                        Total = order.Total
                    }
                ).ToList();
            } catch (Exception e)
            {
                Log.Error(e.Message, "Failed to retrieve list of orders from database.");
                return null;
            }
            return orders;

        }
        public List<Order> GetOrdersFor(User customer)
        {
            List<Order> orders;
            try{
            Log.Debug("Attempting to retrieve list of orders from the database.");
            orders = _context.Orders.Where(
                order => order.UserName == customer.UserName).Select(
                    order => new Order()
                    {
                        OrderNumber = order.OrderNumber,
                        UserName = customer.UserName,
                        StoreID = order.StoreID,
                        Total = order.Total
                    }
                ).ToList();
            } catch (Exception e)
            {
                Log.Error(e.Message, "Failed to retrieve list of orders from database.");
                return null;
            }
            return orders;
        }

        public Product GetProduct(Product item)
        {
            return GetProduct(item.ISBN);
        }
        public Product GetProduct(string ISBN)
        {
            Product found;
            try {
            Log.Debug("Attempting to retrieve product with ISBN: " + ISBN + " from the database.");
            found = _context.Products.FirstOrDefault(product => product.ISBN == ISBN);
            } catch(Exception e)
            {
                Log.Error(e.Message, "Failed to retrieve product with ISBN: " + ISBN +" from database.");
                return null;
            }
            return new Product(found.Price, found.ISBN, found.Name);
        }

        public List<Product> GetProducts()
        {
            List<Product> products;
            try{
            Log.Debug("Attempting to retrieve list of products from the database.");
            products = _context.Products.Select(
                product => new Product()
                { Price = product.Price,
                ISBN = product.ISBN,
                Name = product.Name
                }
            ).ToList();
            } catch(Exception e)
            {
                Log.Error(e.Message, "Failed to retrieve a list of products from database.");
                return null;
            }
            return products;
        }


        public Store GetStore(Store store)
        {
            Store found;
            try {
            Log.Debug("Attempting to retrieve Store: " + store + " from the database.");
            found = _context.Stores.FirstOrDefault(DBStore => DBStore.StoreCity == store.StoreCity &&
            DBStore.StoreState == DBStore.StoreState);
            } catch(Exception e)
            {
                Log.Error(e.Message, "Failed to retrieve Store: " + store + " from database.");
                return null;
            }

            List<Inventory> inventory = getInventory(found.StoreID);
            return new Store(found.StoreCity, found.StoreState, inventory, found.StoreID);
        }
        public Store GetStore(int StoreID)
        {
            try
            {
                Store found = _context.Stores.First(store => StoreID == store.StoreID);
                return found;
            } catch (Exception e)
            {
                Log.Error("Failed to Get Store with ID: " + StoreID);
                return null;
            }
        }

        public List<Transaction> GetTransactions(int OrderNumber)
        {
            List<Transaction> transactions;
            try {
            Log.Debug("Attempting to retrieve Transaction related to OrderNumber: " + OrderNumber + " from the database.");
            transactions = _context.Transactions.Where(
                transaction => transaction.OrderNumber == OrderNumber
            ).Select(
                transaction => new Transaction()
                {
                    ISBN = transaction.ISBN,
                    OrderNumber = transaction.OrderNumber,
                    Quantity = transaction.Quantity
                }
            ).ToList();
            } catch(Exception e)
            {
                Log.Error(e.Message ,"Failed to retrieve Transaction related to OrderNumber: " + OrderNumber + " from the database.");
                return null;
            }
            return transactions;
        }

        public User GetUser(string UserName)
        {
            User found;
            try {
            Log.Debug("Attempting to retrieve User: " + UserName + " from the database.");
            found = _context.Users.FirstOrDefault(user => user.UserName == UserName);
            } catch(Exception e)
            {
                Log.Error(e.Message ,"Failed to retrieve User: " + UserName + " from the database.");
                return null;
            }

            User account = new User(found.UserName, found.Password, found.FirstName, found.LastName, found.Code, found.created);
            if (account == null) return null;
            return account;
        }

        public bool RemoveProduct(Product product)
        {
            Product found;
            try {
            Log.Debug("Attempting to retrieve Product: " + product + " from the database.");
            found = _context.Products.First(prod => prod.ISBN == product.ISBN);
            _context.Products.Remove(found);
            } catch(Exception e)
            {
            Log.Error(e.Message ,"Failed to retrieve Product: " + product + " from the database.");
            return false;
            }
            _context.SaveChanges();
            return true;
        }

        public bool RemoveStore(Store store)
        {
            try
            {
                _context.Stores.Remove(store);
                _context.SaveChanges();
                return true;
            } catch (Exception e)
            {
                Log.Error(e.Message, "Failed to remove Store: " + store + " from database");
                return false;
            }
        }

        public bool UpdateProduct(Product updatedProduct)
        {
            Product found;
            try {
            Log.Debug("Attempting to Update Product: " + updatedProduct + " from the database.");
            found = _context.Products.Find(updatedProduct.ISBN);
            if (found != null){
                found.Name = updatedProduct.Name;
                found.Price = updatedProduct.Price;
                _context.SaveChanges();
            }
            } catch (Exception e)
            {   
                Log.Error(e.Message ,"Failed to Update Product: " + updatedProduct + " from the database.");
                return false;
            }
            return true;
        }

        public bool UpdateInventory(Inventory inventory)
        {   
            Models.Inventory found;
            try{
            Log.Debug("Attempting to Update Inventory: " + inventory + " from the database.");
            found = _context.Inventories.FirstOrDefault(inv => inv.ISBN == inventory.ISBN && inv.StoreID == inventory.StoreID);
            found.Quantity = inventory.Quantity;
            found.ISBN = inventory.ISBN;
            found.StoreID = inventory.StoreID;
            } catch (Exception e)
            {
                Log.Error(e.Message , "Failed to Update Inventory: " + inventory + " from the database.");
                return false;
            }
            _context.SaveChanges();
            return true;
        }

        public bool UpdateStore(Store store)
        {
            try
            {
                _context.Stores.Update(store);
                _context.SaveChanges();
                return true;
            } catch (Exception e)
            {
                Log.Error(e.Message, "Failed to Update with Store: " + store);
                return false;
            }
        }

        public bool AddInventoryItem(Inventory item)
        {
            try
            {
                Log.Debug("Attempting to add item with ISBN: " + item.ISBN + " to Store with ID: " + item.StoreID);
                _context.Inventories.Add(item);
                _context.SaveChanges();
                return true;
            } catch (Exception e)
            {
                Log.Error(e.Message, "Failed to add item ISBN: " + item.ISBN + " to store ID: " + item.StoreID);
                return false;
            }
        }

        public bool RemoveInventoryItem(Inventory item)
        {
            try
            {
                _context.Inventories.Remove(item);
                _context.SaveChanges();
                return true;
            } catch (Exception e)
            {
                Log.Error(e.Message, "Failed to remove item ISBN: " + item.ISBN + " from store ID: " + item.StoreID);
                return false;
            }
        }

        public Transaction GetTransaction(int ordernumber, string ISBN)
        {
            try
            {
                return _context.Transactions.FirstOrDefault(transact => transact.ISBN == ISBN && transact.OrderNumber == ordernumber);
            } catch (Exception e)
            {
                Log.Error(e.Message, "Failed to get Transaction with Ordernumber: " + ordernumber + " And ISBN: " + ISBN);
                return null;
            }
        }

        public Transaction UpdateTransaction(Transaction transact)
        {
            _context.Transactions.Update(transact);
            _context.SaveChanges();
            return transact;
        }

        public Order GetOrder(int OrderNumber)
        {
            return _context.Orders.FirstOrDefault(order => order.OrderNumber == OrderNumber);
        }
    }
}