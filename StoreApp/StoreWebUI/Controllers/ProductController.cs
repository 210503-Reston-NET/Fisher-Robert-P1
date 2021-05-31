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
    public class ProductController : Controller
    {
     
        private StoreBLInterface _storeBL;
        public ProductController(StoreBLInterface storeBL)
        {
            this._storeBL = storeBL;
        }
        // GET: StoreController
        public ActionResult Index(int id)
        {
            return View(_storeBL.GetAllProducts()
                .Select(product => new ProductVM(product))
                .ToList());
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        { 
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductVM item)
        {
            try
            {
                    if (ModelState.IsValid)
                    {
                        Console.WriteLine("ModelState Is Valid");
                    _storeBL.AddProduct(new Product
                    {
                        ISBN = item.ISBN,
                        Name = item.Name,
                        Price = item.Price
                    });
                    }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_storeBL.GetStore(id));
        }

        // POST: ProductController/Edit/5
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

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, StoreVM storeVM)
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
    }
}
