using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreBL;
using StoreWebUI.Models;
using StoreModels;

namespace StoreWebUI.Controllers
{
    public class InventoryController : Controller
    {
     
        private StoreBLInterface _storeBL;
        public InventoryController(StoreBLInterface storeBL)
        {
            this._storeBL = storeBL;
        }
        // GET: StoreController
        public ActionResult Index(int id)
        {
            return View(_storeBL.GetInventoryFor(id)
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
        public ActionResult Create(InventoryVM item)
        {
            try
            {
                    if (ModelState.IsValid)
                    {
                        Console.WriteLine("ModelState Is Valid");
                    _storeBL.AddInventoryItem(new Inventory
                    {
                        ISBN = item.ISBN,
                        StoreID = item.StoreID,
                        Quantity = item.Quantity
                    });
                    }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StoreController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_storeBL.GetStore(id));
        }

        // POST: StoreController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, StoreVM storeVM)
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

        // GET: StoreController/Delete/5
        public ActionResult Delete(int id)
        {
            //return View(new StoreVM(_storeBL.GetStore(id)));
            return View(_storeBL.GetStore(id));
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
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
