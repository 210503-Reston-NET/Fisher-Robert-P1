using System;
using System.Collections.Generic;
using System.Linq;
using StoreModels;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoreWebUI.Models
{
    public class ProductVM
    {
        public ProductVM(decimal Price, string ISBN, string Name)
        {
            this.Price = Price;
            this.ISBN = ISBN;
            this.Name = Name;
        }
        public ProductVM() { }
        public ProductVM(Product item)
        {
            this.ISBN = item.ISBN;
            this.Name = item.Name;
            this.Price = item.Price;
        }

        [Required]
        public decimal Price { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
