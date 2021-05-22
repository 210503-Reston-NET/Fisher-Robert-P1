using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreModels;

namespace StoreWebUI.Models
{
    public class StoreVM
    {
        public StoreVM()
        { }
        public StoreVM(Store store)
        {
            StoreID = store.StoreID;
            StoreCity = store.StoreCity;
            Inventory = store.Inventory;
            StoreState = store.StoreState;
        }
        public string StoreCity { get; set; }
        public string StoreState { get; set; }
        public List<Inventory> Inventory { get; set; }
        public int StoreID { get; set; }
    }
}
