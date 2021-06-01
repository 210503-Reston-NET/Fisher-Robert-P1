using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreBL;
using StoreWebUI.Models;
using StoreModels;
using Microsoft.Extensions.Logging;

namespace StoreWebUI.Controllers
{
    public class InventoryController : Controller
    {
     
        private StoreBLInterface _storeBL;
        private readonly ILogger<HomeController> _logger;
        public InventoryController(ILogger<HomeController> logger, StoreBLInterface storeBL)
        {
            this._storeBL = storeBL;
            _logger = logger;
        }
        // GET: StoreController/5
        public ActionResult Index(int id)
        {
            // If id has been sent then set the storeID session to the new id
            if (id > -1)
            {
                HttpContext.Session.SetString("StoreID", id + "");
                return View(_storeBL.GetInventoryFor(id)
                    .Select(inventory => new InventoryVM(inventory))
                    .ToList());
            }
            // Otherwise use previous StoreID
            return View(_storeBL.GetInventoryFor(int.Parse(HttpContext.Session.GetString("StoreID")))
                .Select(inventory => new InventoryVM(inventory))
                .ToList());
        }

        // GET: InventoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: InventoryController/Create
        public ActionResult Create()
        {
            ViewBag.products = _storeBL.GetAllProducts();
            return View();
        }

        // POST: InventoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string ISBN)
        {
            try
            {
                _storeBL.AddInventoryItem(new Inventory
                    {
                        ISBN = ISBN,
                        StoreID = int.Parse(HttpContext.Session.GetString("StoreID")),
                        Quantity = 0
                    });

                return Redirect("Inventory/Index/" + HttpContext.Session.GetString("StoreID"));
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to create Inventory\n" + e.Message);
                return View();
            }
        }

        // GET: StoreController/Edit/5
        public ActionResult Edit(int id, string ISBN)
        {
            return View(_storeBL.GetInventory(id, ISBN));
        }

        // POST: StoreController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, InventoryVM inventoryVM)
        {
            try
            {
                Inventory result = new Inventory()
                {
                    ISBN = inventoryVM.ISBN,
                    StoreID = inventoryVM.StoreID,
                    Quantity = inventoryVM.Quantity
                };
                _storeBL.UpdateInventory(result);
                return Redirect("/Inventory/Index/" + HttpContext.Session.GetString("StoreID"));
            }
            catch
            {
                return View();
            }
        }

        // GET: StoreController/Delete/5
        public ActionResult Delete(int id, string ISBN)
        {
            return View(_storeBL.GetInventory(id, ISBN));
        }

        // POST: StoreController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, StoreVM storeVM)
        {
            try
            {
                _storeBL.RemoveStore(new Store()
                {
                    StoreID = storeVM.StoreID,
                    StoreCity = storeVM.StoreCity,
                    StoreState = storeVM.StoreState
                });
                return Redirect("Inventory/Index/" + HttpContext.Session.GetString("StoreID"));
            }
            catch
            {
                return View();
            }
        }
    }
}
