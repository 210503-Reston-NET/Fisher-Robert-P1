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
    public class StoreController : Controller
    {
     
        private StoreBLInterface _storeBL;
        public StoreController(StoreBLInterface storeBL)
        {
            this._storeBL = storeBL;
        }
        // GET: StoreController
        public ActionResult Index()
        {
            return View(_storeBL.GetAllStores()
                .Select(store => new StoreVM(store))
                .ToList()
                );
        }

        // GET: StoreController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StoreController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StoreController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StoreVM storeVM)
        {
            try
            {
                    if (ModelState.IsValid)
                    {
                        Console.WriteLine("ModelState Is Valid");
                        _storeBL.AddStore(new Store
                        {
                            StoreCity = storeVM.StoreCity,
                            StoreState = storeVM.StoreState,
                            StoreID = storeVM.StoreID,
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
                _storeBL.UpdateStore(new Store()
                {
                    StoreCity = storeVM.StoreCity,
                    StoreState = storeVM.StoreState,
                    StoreID = storeVM.StoreID
                });
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
