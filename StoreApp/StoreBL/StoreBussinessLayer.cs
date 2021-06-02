using System.Collections.Generic;
using StoreModels;
using StoreDL;
using System;

namespace StoreBL
{
    public class StoreBussinessLayer : StoreBLInterface
    {
        private DAO _repoDB;
        public StoreBussinessLayer(DAO repo)
        {
            _repoDB = repo;
        }
        public List<Inventory> GetInventory(int storeID)
        {
            return _repoDB.getInventory(storeID);
        }
        public bool AddProduct(Product product)
        {
            return _repoDB.AddProduct(product);
        }
        public bool AddInventoryItem(Inventory item)
        {
            return _repoDB.AddInventoryItem(item);
        }
        public Order AddOrder(Order order)
        {
            return _repoDB.AddOrder(order);
        }

        public Product GetProduct(string ISBN)
        {
            return _repoDB.GetProduct(ISBN);
        }

        public List<Product> GetAllProducts()
        {
            return _repoDB.GetProducts();
        }

        public User GetUser(string UserName, string Password)
        {
            return _repoDB.GetUser(UserName);
        }

        public List<User> GetAllUsers()
        {
            return _repoDB.GetAllUsers();
        }

        public bool AddUser(User user)
        {
            return _repoDB.AddUser(user);
        }

        public Store GetStore(Store TargetStore)
        {
            return _repoDB.GetStore(TargetStore);
        }

        public Order GetOrder(Order order)
        {
            Order result = _repoDB.GetOrder(order);
            result.Transactions = GetTransactions(order.OrderNumber);

            return result;
        }

        public bool AddTransaction(Transaction transact)
        {
            return _repoDB.AddTransaction(transact);
        }

        public bool RemoveProduct(Product product)
        {
            try {
                _repoDB.RemoveProduct(product);
            } catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }
        public bool RemoveStore(Store store)
        {
            return _repoDB.RemoveStore(store);
        }

        public bool UpdateProduct(Product EditedProduct)
        {
            return _repoDB.UpdateProduct(EditedProduct);
        }

        public List<Store> GetAllStores()
        {
            List<Store> stores = _repoDB.GetAllStores();

                foreach (Store store in stores)
                    store.Inventory = GetInventory(store.StoreID);

            return stores;
        }

        public List<Order> GetAllOrders(int storeID)
        {
            List<Order> found = _repoDB.GetOrdersFor(storeID);

            foreach(Order order in found)
                order.Transactions = _repoDB.GetTransactions(order.OrderNumber);
            
            return found;
        }

        public List<Inventory> GetInventoryFor(int storeID)
        {
            return _repoDB.getInventory(storeID);
        }
        public Store GetStore(int StoreID)
        {
            return _repoDB.GetStore(StoreID);
        }
        public List<Transaction> GetTransactions(int OrderNumber)
        {
            return _repoDB.GetTransactions(OrderNumber);
        }

        public Inventory GetInventory(int storeId, string ISBN)
        {
            return _repoDB.GetInventory(storeId, ISBN);
        }

        public bool UpdateInventory(Inventory inventory)
        {
            return _repoDB.UpdateInventory(inventory);
        }

        public List<Order> GetAllOrders(User user)
        {
            return _repoDB.GetOrdersFor(user);
        }

        public bool AddStore(Store store)
        {
            return _repoDB.AddStore(store);
        }

        public bool UpdateStore(Store store)
        {
            return _repoDB.UpdateStore(store);
        }

        public bool RemoveInventoryItem(Inventory item)
        {
            return _repoDB.RemoveInventoryItem(item);
        }

        public Transaction GetTransaction(int ordernumber, string ISBN)
        {
            return _repoDB.GetTransaction(ordernumber, ISBN);
        }

        public Transaction UpdateTransaction(Transaction trasnact)
        {
            return _repoDB.UpdateTransaction(trasnact);
        }

        public Order GetOrder(int OrderNumber)
        {
            return _repoDB.GetOrder(OrderNumber);
        }

        public List<Order> GetAllOrders()
        {
            return _repoDB.GetAllOrders();
        }

        public List<Transaction> GetAllTransactions()
        {
            return _repoDB.GetAllTransactions();
        }

        public Order UpdateOrder(Order order)
        {
            return _repoDB.UpdateOrder(order);
        }

        public List<Order> OrderedListofOrders(string order, string by)
        {
            return _repoDB.OrderedListofOrders(order, by);
        }
    }
}