using System.Collections.Generic;
using System;

namespace StoreModels
{
    public class Store
    {
        public string StoreCity { get; set; }
        public string StoreState { get; set; }
        public List<Inventory> Inventory { get; set; }
        public int StoreID { get; set; }

        public Store() { }
        public Store (string StoreCity, string StoreState)
        {
            this.StoreCity = StoreCity;
            this.StoreState = StoreState;
            this.Inventory = new List<Inventory>();
            this.StoreID = -1;
        }
        public Store (string StoreCity, string StoreState, int storeID): this(StoreCity, StoreState)
        {
            this.StoreID = storeID;
        }
        public Store (string StoreName, string Address, List<Inventory> inventory) : this(StoreName, Address)
        {
            this.Inventory = inventory;
        }
        public Store (string StoreName, string Address, List<Inventory> inventory, int storeID) : this(StoreName, Address, inventory)
        {
            this.StoreID = storeID;
        }
        public override string ToString()
        {
            return "City: " + this.StoreCity + "\nState: " + this.StoreState + "\nstoreID: " + this.StoreID;
        }

    }
}