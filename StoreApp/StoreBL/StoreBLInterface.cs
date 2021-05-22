using System.Collections.Generic;
using StoreModels;

namespace StoreBL
{
    public interface StoreBLInterface
    {
        List<Inventory> GetInventory (int storeId);
        Inventory GetInventory (int storeId, string ISBN);
        bool AddProduct(Product product);
        Product GetProduct(string ISBN);
        bool RemoveProduct(Product product);
        public bool RemoveStore(Store store);
        bool UpdateProduct(Product EditedProduct);
        bool UpdateInventory(Inventory inventory);
        bool UpdateStore(Store store);
        List<Product> GetAllProducts();
        User GetUser(string UserName, string Password);
        List<User> GetAllUsers();
        bool AddUser(User user);
        bool AddStore(Store store);
        Order AddOrder(Order order);
        bool AddTransaction(Transaction transact);
        Store GetStore(Store TargetStore);
        public Store GetStore(int StoreID);
        List<Store> GetAllStores();
        Order GetOrder(Order order);
        List<Order> GetAllOrders(int storeID);
        List<Order> GetAllOrders(User user);
        List<Inventory> GetInventoryFor(int storeID);
        List<Transaction> GetTransactions(int OrderNumber);
    }
}