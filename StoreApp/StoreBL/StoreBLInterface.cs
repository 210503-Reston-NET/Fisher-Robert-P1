using System.Collections.Generic;
using StoreModels;

namespace StoreBL
{
    public interface StoreBLInterface
    {
        /// <summary>
        /// Gets a list of inventory items related by StoreID
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        List<Inventory> GetInventory (int storeId);
        /// <summary>
        /// Returns an Inventory Item for given storeID and ISBN
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="ISBN"></param>
        /// <returns></returns>
        Inventory GetInventory (int storeId, string ISBN);
        /// <summary>
        /// Adds the product given to the database
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        bool AddProduct(Product product);
        /// <summary>
        /// Returns a Product Object for the given Primary Key 'ISBN'
        /// </summary>
        /// <param name="ISBN"></param>
        /// <returns></returns>
        Product GetProduct(string ISBN);
        /// <summary>
        /// Removes Given Product from Database 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        bool RemoveProduct(Product product);
        /// <summary>
        /// Removes Given Store from Database
        /// </summary>
        /// <param name="store"></param>
        /// <returns></returns>
        public bool RemoveStore(Store store);
        /// <summary>
        /// Updates the Given Product in the Database
        /// </summary>
        /// <param name="EditedProduct"></param>
        /// <returns></returns>
        bool UpdateProduct(Product EditedProduct);
        /// <summary>
        /// Update the Given Inventory in the Database
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        bool UpdateInventory(Inventory inventory);
        /// <summary>
        /// Updates The Given Store in the Database
        /// </summary>
        /// <param name="store"></param>
        /// <returns></returns>
        bool UpdateStore(Store store);
        /// <summary>
        /// Returns a List of all Product Objects from the Database
        /// </summary>
        /// <returns></returns>
        List<Product> GetAllProducts();
        /// <summary>
        /// Returns a User Object for givent UserName and Password from Database
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        User GetUser(string UserName, string Password);
        /// <summary>
        /// Returns a List of all User Objects from Database
        /// </summary>
        /// <returns></returns>
        List<User> GetAllUsers();
        /// <summary>
        /// Adds Given User Object to Database and returns true if returned
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool AddUser(User user);
        /// <summary>
        /// Adds Given Store Object to the Database and returns true if returned
        /// </summary>
        /// <param name="store"></param>
        /// <returns></returns>
        bool AddStore(Store store);
        /// <summary>
        /// Adds Given Order to the Database and returns the Object
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Order AddOrder(Order order);
        /// <summary>
        /// Adds a Given Transaction to the Database
        /// </summary>
        /// <param name="transact"></param>
        /// <returns></returns>
        bool AddTransaction(Transaction transact);
        /// <summary>
        /// Returns a Store Object from the Database with a given Store Object reference
        /// </summary>
        /// <param name="TargetStore"></param>
        /// <returns></returns>
        Store GetStore(Store TargetStore);
        /// <summary>
        /// Returns a Store Object from the Database that has the given StoreID
        /// </summary>
        /// <param name="StoreID"></param>
        /// <returns></returns>
        Store GetStore(int StoreID);
        /// <summary>
        /// Returns a List of all Store Objects from the Database
        /// </summary>
        /// <returns></returns>
        List<Store> GetAllStores();
        /// <summary>
        /// Returns and Order from the Database that matches Given Order Object
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Order GetOrder(Order order);
        /// <summary>
        /// Returns a List of Order Objects from the Database with the given StoreID
        /// </summary>
        /// <param name="storeID"></param>
        /// <returns></returns>
        List<Order> GetAllOrders(int storeID);
        /// <summary>
        /// Returns a List of Order Objects from the Database that relate to the Given User Object
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        List<Order> GetAllOrders(User user);
        /// <summary>
        /// Returns a List of Inventory Objects from the Database with the given store ID
        /// </summary>
        /// <param name="storeID"></param>
        /// <returns></returns>
        List<Inventory> GetInventoryFor(int storeID);
        /// <summary>
        /// Returns a List of Transaction Objects from the Database that have a given OrderNumber
        /// </summary>
        /// <param name="OrderNumber"></param>
        /// <returns></returns>
        List<Transaction> GetTransactions(int OrderNumber);
        /// <summary>
        /// Adds the given Inventory Object to the Database and returns true if successful
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool AddInventoryItem(Inventory item);
        /// <summary>
        /// Removes the given Item from the Database and returns true if successful
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool RemoveInventoryItem(Inventory item);
        /// <summary>
        /// Returns a Transaction Object from the Database with Given ordernumber and ISBN
        /// </summary>
        /// <param name="ordernumber"></param>
        /// <param name="ISBN"></param>
        /// <returns></returns>
        Transaction GetTransaction(int ordernumber, string ISBN);
        /// <summary>
        /// Returns a Transaction Object from the Database that matches the given transact
        /// </summary>
        /// <param name="trasnact"></param>
        /// <returns></returns>
        Transaction UpdateTransaction(Transaction trasnact);
        /// <summary>
        /// Returns an Order Object with a given OrderNumber
        /// </summary>
        /// <param name="OrderNumber"></param>
        /// <returns></returns>
        Order GetOrder(int OrderNumber);
        /// <summary>
        /// Returns a List of Order Objects from the Database
        /// </summary>
        /// <returns></returns>
        List<Order> GetAllOrders();
        /// <summary>
        /// Returns a List of Transaction Objects 
        /// </summary>
        /// <returns></returns>
        List<Transaction> GetAllTransactions();
        /// <summary>
        /// Returns Order Object from Database
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Order UpdateOrder(Order order);
        /// <summary>
        /// Returns an Ordered List of Order Objects 
        /// </summary>
        /// <param name="order"></param>
        /// <param name="by"></param>
        /// <returns></returns>
        List<Order> OrderedListofOrders(string order, string by);
        /// <summary>
        /// Returns an Ordered List of Order Objects
        /// </summary>
        /// <param name="order"></param>
        /// <param name="by"></param>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        List<Order> OrderedListofOrders(string order, string by, string UserName);
    }
}