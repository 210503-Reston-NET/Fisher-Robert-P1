using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

        [DisplayName("City")]
        [Required]
        public string StoreCity { get; set; }
        [DisplayName("State")]
        [Required]
        public string StoreState { get; set; }
        public List<Inventory> Inventory { get; set; }
        [Required]
        public int StoreID { get; set; }
    }
}
