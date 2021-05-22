using StoreModels;
using StoreBL;
using System.Collections.Generic;

namespace StoreUI
{
    public class CustomerOrdersMenu : StoreMenu
    {
        MyValidate validate = new StringValidator();
        StoreBLInterface bussinessLayer;
        public CustomerOrdersMenu(StoreBLInterface BL, User passedUser)
        {
            this.bussinessLayer = BL;
            this.CurrentUser = passedUser;
        }
        public override void Start()
        {
            List<Order> orders = bussinessLayer.GetAllOrders(this.CurrentUser);

            System.Console.WriteLine("--------Order History--------");

            foreach (Order order in orders)
            {
                order.Transactions = bussinessLayer.GetTransactions(order.OrderNumber);
            }

            foreach (Order order in orders)
            {
                System.Console.WriteLine("--------Order: " + order.OrderNumber + "--------");
                System.Console.WriteLine(order);
                System.Console.WriteLine("----Transactions----");
                foreach(Transaction transact in order.Transactions)
                {
                    System.Console.WriteLine("\t" + transact);
                }
                System.Console.WriteLine();
            }
        }
    }
}