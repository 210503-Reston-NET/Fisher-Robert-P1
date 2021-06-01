using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoreWebUI.Models
{
    public class TransactionVM
    {
        [Required]
        [Key]
        public int OrderNumber { get; set; }
        [Required]
        [Key]
        public string ISBN { get; set; }
        [Required]
        public int? Quantity { get; set; }
        public TransactionVM() { }
        public TransactionVM(int ON, string isbn, int qty) : this(isbn, qty)
        {
            this.Quantity = qty;
        }
        public TransactionVM(string isbn, int qty)
        {
            this.ISBN = isbn;
            this.Quantity = qty;
        }
    }
}
