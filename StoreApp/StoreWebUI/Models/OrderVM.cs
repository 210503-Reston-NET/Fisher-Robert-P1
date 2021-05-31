using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoreWebUI.Models
{
    public class OrderVM
    {
        [Required]
        public int OrderNumber { get; set; }
        [Required]
        public string UserName { get; set; }
        [DisplayName("Related Store ID")]
        [Required]
        public int StoreID { get; set; }
        [Required]
        [DisplayName("Date Purchased")]
        public DateTime Create { get; set; }
        public List<TransactionVM> Transactions { get; set; }
        [Required]
        public decimal Total { get; set; }

        public OrderVM()
        {
            this.Create = DateTime.Now;
        }
        public OrderVM(int OrderNumber, int StoreId, string CustomerId, decimal total, DateTime Created) : this(StoreId, CustomerId, total)
        {
            this.OrderNumber = OrderNumber;
            this.Create = Created;
        }
        public OrderVM(int OrderNumber, int StoreId, string CustomerId, decimal total, List<TransactionVM> transacitons) : this(OrderNumber, StoreId, CustomerId, total, DateTime.Now)
        {
            this.Transactions = transacitons;
        }
        public OrderVM(int StoreId, string CustomerId, decimal total)
        {
            this.StoreID = StoreID;
            this.UserName = CustomerId;
            this.Total = total;
        }
    }
}
