using System.Collections.Generic;
using StoreModels;
using System.IO; 
using System.Text.Json; 
using System;
using System.Linq;

namespace StoreDL
{
    public class FileRepo 
    {
        private const string ProductFilePath = "../StoreDL/JsonFiles/Products.json";
        private const string OrderFilePath = "../StoreDL/JsonFiles/Orders.json";
        private const string CustomerFilePath = "../StoreDL/JsonFiles/Customers.json";
        private string JsonString;
        /// <summary>
        /// Returns all products recorded
        /// </summary>
        public List<Product> GetProducts()
        {
            try
            {
                JsonString = File.ReadAllText(ProductFilePath);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return new List<Product>();
            }
            return JsonSerializer.Deserialize<List<Product>>(JsonString);
        }
        /// <summary>
        /// Returns the product found given the specific ISBN
        /// </summary>
        public Product GetProduct(string ISBN)
        {
            return GetProducts().FirstOrDefault(pr => pr.ISBN.Equals(ISBN));
        }
        /// <summary>
        /// Creates an instance of a certain product and then writes it down
        /// </summary>
        public bool AddProduct(Product product)
        {
            try {
                List<Product> ProductsFromFile = GetProducts();
                ProductsFromFile.Add(product);
                JsonString = JsonSerializer.Serialize(ProductsFromFile);
                File.WriteAllText(ProductFilePath, JsonString);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public Order GetOrder(string OrderNumber)
        {
            throw new NotImplementedException();
        }
        public List<Order> GetOrders()
        {
            try
            {
                JsonString = File.ReadAllText(OrderFilePath);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return new List<Order>();
            }
            return JsonSerializer.Deserialize<List<Order>>(JsonString);
        }

        public List<Order> GetOrdersFor(User Customer)
        {
            throw new NotImplementedException();
        }

        public bool AddOrder(Order order)
        {
            try {
                List<Order> ProductsFromFile = GetOrders();
                ProductsFromFile.Add(order);
                JsonString = JsonSerializer.Serialize(ProductsFromFile);
                File.WriteAllText(OrderFilePath, JsonString);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public bool AddStore(Store store)
        {
            throw new NotImplementedException();
        }

        public Store GetStore(Store store)
        {
            throw new NotImplementedException();
        }

        public List<Store> GetAllStores()
        {
            throw new NotImplementedException();
        }
    }
}