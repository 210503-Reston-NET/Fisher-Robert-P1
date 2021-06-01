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

        // GET: OrderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/ShopList
        public ActionResult ShopList(int iD)
        {
            List<Inventory> storeStocks = _BL.GetInventoryFor(iD);
            List<Product> products = new List<Product>();
            
            foreach(Inventory item in storeStocks)
            {
                products.Add(_BL.GetProduct(item.ISBN));
            }

            ViewBag.inventory = storeStocks;
            return View(products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StoreSelector(int ID)
        {
            return RedirectToAction(nameof(ShopList), new { id = ID });
        }


    }
}
