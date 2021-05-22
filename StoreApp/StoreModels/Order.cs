using System;
using System.Collections.Generic;

namespace StoreModels
{
    public class Order
    {
        public int OrderNumber { get; set; }
        public string UserName { get; set; }
        public int StoreID { get; set; }
        public DateTime Create { get; set;}
        public List<Transaction> Transactions { get; set;}
        public decimal Total { get; set; }
        
        public Order(){
            this.Create = DateTime.Now;
        }
        public Order(int OrderNumber, int StoreId, string CustomerId, decimal total, DateTime Created): this(StoreId, CustomerId, total)
        {
            this.OrderNumber = OrderNumber;
            this.Create = Created;
        }
        public Order(int OrderNumber, int StoreId, string CustomerId, decimal total, List<Transaction> transacitons): this(OrderNumber, StoreId, CustomerId, total, DateTime.Now)
        {
            this.Transactions = transacitons;
        }
        public Order(int StoreId, string CustomerId, decimal total)
        {
            this.StoreID = StoreID;
            this.UserName = CustomerId;
            this.Total = total;
        }
        public override string ToString()
        {
            return "OrderNumber: " + this.OrderNumber + "\tUserName: " + this.UserName + "\nStoreID: " + this.StoreID + "\nCreated: " + this.Create;
        }
    }
}