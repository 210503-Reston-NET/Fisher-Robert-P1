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
using Serilog;

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
            try
            {
                List<Inventory> storeStocks = _BL.GetInventoryFor(int.Parse(HttpContext.Session.GetString("StoreID")));
                List<Product> products = new List<Product>();

                ViewBag.inventory = storeStocks;

                Log.Information("Collecting products to fill inventory for the current Sessioned StoreID");
                // Add products related to inventory
                foreach (Inventory inv in storeStocks)
                    if (inv.StoreID == int.Parse(HttpContext.Session.GetString("StoreID")))
                        products.Add(_BL.GetProduct(inv.ISBN));

                return View(products);
            } catch (Exception e)
            {
                Log.Error(e.Message, "Failed to gather products to fill inventory list");
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StoreSelector(int ID)
        {
            Log.Information("Current Sessioned StoreID changed to: " + ID);
            HttpContext.Session.SetString("StoreID", ID + "");

            try
            {
                Order newOrder = _BL.AddOrder(new Order()
                {
                    StoreID = int.Parse(HttpContext.Session.GetString("StoreID")),
                    UserName = HttpContext.Session.GetString("UserName"),
                    Total = 0,
                    Create = DateTime.Now,
                    Transactions = new List<Transaction>()
                });

                Log.Information("Current Sessioned OrderNumber changed to: " + newOrder.OrderNumber);
                HttpContext.Session.SetString("OrderNumber", newOrder.OrderNumber + "");
            }
            catch (Exception e)
            {
                Log.Error(e.Message, "Failed to create a new Order");
                return View();
            }

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
                // Checks to see if transactions exceed the current inventory
                Transaction updatableTransact = _BL.GetTransaction(int.Parse(HttpContext.Session.GetString("OrderNumber")), id);
                if (updatableTransact.Quantity < _BL.GetInventory(int.Parse(HttpContext.Session.GetString("StoreID")), Isbn).Quantity)
                    updatableTransact.Quantity++;
                else
                    Console.WriteLine("You have exceeded the maximum purchase limit.");

                // Update Transactions
                _BL.UpdateTransaction(updatableTransact);
            }


            // There is no such transaction yet, so add it as a new transaction item
            else if (_BL.GetTransaction(int.Parse(HttpContext.Session.GetString("OrderNumber")), id) == null)
                try
                {
                    _BL.AddTransaction(new Transaction()
                    {
                        ISBN = Isbn,
                        OrderNumber = int.Parse(HttpContext.Session.GetString("OrderNumber")),
                        Quantity = 1
                    });
                } catch(Exception e)
                {
                    Log.Error(e.Message, "Failed to add new Transaction while purchasing");
                }
            // Update Inventory 
            Inventory inv = _BL.GetInventory(int.Parse(HttpContext.Session.GetString("StoreID")), Isbn);
            inv.Quantity--;
            _BL.UpdateInventory(inv);

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

        public ActionResult CustomerOrders(string order, string by, string id)
        {
            ViewBag.Stores = _BL.GetAllStores();
            ViewBag.Products = _BL.GetAllProducts();
            ViewBag.Transactions = _BL.GetAllTransactions();

            Log.Information("Attempting to pull Orders for Customer: " + id);

            return View(_BL.OrderedListofOrders(by, order, id));
        }

        public ActionResult ViewTransactions(int orderNumber)
        {
            ViewBag.Products = _BL.GetAllProducts();
            return View(_BL.GetTransactions(orderNumber));
        }

    }
}
