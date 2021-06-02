using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreModels;
using StoreWebUI.Models;
using StoreBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StoreWebUI.Controllers
{
    public class OrderController : Controller
    {
        StoreBLInterface _BL;
        public OrderController(StoreBLInterface BL)
        {
            _BL = BL;
        }
        // GET: OrderController
        public ActionResult Index()
        {
            return View(_BL.GetAllStores());
        }

        // GET: Order/ShopList
        public ActionResult ShopList(List<Transaction> currentOrder)
        {
            List<Inventory> storeStocks = _BL.GetInventoryFor(int.Parse(HttpContext.Session.GetString("StoreID")));
            List<Product> products = new List<Product>();

            ViewBag.inventory = storeStocks;

            foreach (Inventory inv in storeStocks)
                if (inv.StoreID == int.Parse(HttpContext.Session.GetString("StoreID")))
                    products.Add(_BL.GetProduct(inv.ISBN));

            return View(products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StoreSelector(int ID)
        {
            HttpContext.Session.SetString("StoreID", ID + "");

            Order newOrder = _BL.AddOrder(new Order()
            {
                StoreID = int.Parse(HttpContext.Session.GetString("StoreID")),
                UserName = HttpContext.Session.GetString("UserName"),
                Total = 0,
                Create = DateTime.Now,
                Transactions = new List<Transaction>()
            });

            HttpContext.Session.SetString("OrderNumber", newOrder.OrderNumber + "");

            return RedirectToAction(nameof(ShopList), new { id = ID});
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult purchase(string id, List<Transaction> currentOrder)
        {
            string Isbn = id;

            // CheckList:
                // 1: Does this transaction Exist
                // 2: will this transaction exceed the inventory
                // 3: Update Inventory
                // 4: Update Order Total
            if (_BL.GetTransaction(int.Parse(HttpContext.Session.GetString("OrderNumber")), id) != null)
            {
                
                Transaction updatableTransact = _BL.GetTransaction(int.Parse(HttpContext.Session.GetString("OrderNumber")), id);
                if (updatableTransact.Quantity < _BL.GetInventory(int.Parse(HttpContext.Session.GetString("StoreID")), Isbn).Quantity)
                    updatableTransact.Quantity++;
                else
                    Console.WriteLine("You have exceeded the maximum purchase limit.");
                
                _BL.UpdateTransaction(updatableTransact);

                // Update Inventory 
                Inventory inv = _BL.GetInventory( int.Parse(HttpContext.Session.GetString("StoreID")), Isbn);
                inv.Quantity--;
                _BL.UpdateInventory(inv);
            }

            
                
            else if (_BL.GetTransaction(int.Parse(HttpContext.Session.GetString("OrderNumber")), id) == null)
                _BL.AddTransaction(new Transaction()
                    {
                        ISBN = Isbn,
                        OrderNumber = int.Parse(HttpContext.Session.GetString("OrderNumber")),
                        Quantity = 1
                    });

            // Update Order Total
            Order current = _BL.GetOrder(int.Parse(HttpContext.Session.GetString("OrderNumber")));
            current.Total = current.Total + _BL.GetProduct(id).Price;
            _BL.UpdateOrder(current);

            return Redirect(Url.Action("ShopList", "Order"));
        }

        public ActionResult ShoppingCart()
        {
            ViewBag.Products = _BL.GetAllProducts();
            return View(_BL.GetTransactions(int.Parse(HttpContext.Session.GetString("OrderNumber"))));
        }

        public ActionResult ViewOrders(string order, string by)
        {
            ViewBag.Stores = _BL.GetAllStores();
            ViewBag.Products = _BL.GetAllProducts();
            ViewBag.Transactions = _BL.GetAllTransactions();

            return View(_BL.OrderedListofOrders(by, order));
        }

        public ActionResult ViewTransactions(int orderNumber)
        {
            ViewBag.Products = _BL.GetAllProducts();
            return View(_BL.GetTransactions(orderNumber));
        }

    }
}
