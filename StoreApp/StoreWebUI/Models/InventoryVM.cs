using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using StoreModels;

namespace StoreWebUI.Models
{
    public class InventoryVM
    {
        [Required]
        [DisplayName("Related Store ID")]
        public int StoreID { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public int? Quantity { get; set; }
        public InventoryVM() { }
        public InventoryVM(int ID, string isbn, int? Quantity)
        {
            this.StoreID = ID;
            this.ISBN = isbn;
            this.Quantity = Quantity;
        }
        public InventoryVM(Inventory given)
        {
            this.StoreID = given.StoreID;
            this.ISBN = given.ISBN;
            this.Quantity = given.Quantity;
        }
    }
}
