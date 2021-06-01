using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreWebUI.Models;
using StoreModels;
using StoreBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StoreWebUI.Controllers
{
    public class UserController : Controller
    {
        StoreBLInterface _BL;
        public UserController(StoreBLInterface bussinessLayer)
        {
            _BL = bussinessLayer;
        }
        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserVM VM)
        {
            try
            {
                
                User user = new User(VM.UserName, VM.Password, VM.FirstName, VM.LastName, VM.Code);
                foreach (User account in _BL.GetAllUsers())
                    if (account == user)
                        return View();
                _BL.AddUser(user);
                return Redirect("../Home/Index");
            }
            catch
            {
                return View();
            }   
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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

        public ActionResult LoginPage()
        {
            Console.WriteLine(HttpContext.Session.GetString("UserName") + "If Blank there is no user");
            if (HttpContext.Session.GetString("UserName") == null)
                return View("../User/LoginPage");
            return Redirect("../Order/Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginPage(UserVM given)
        {
            List<User> accounts = _BL.GetAllUsers();
            if (accounts == null)
                return View();
            foreach (User user in accounts)
            {
                if (user.UserName == given.UserName && user.Password == given.Password)
                {
                    HttpContext.Session.SetString("EmployeeID", JsonConvert.SerializeObject(user.Code));
                    HttpContext.Session.SetString("UserName", JsonConvert.SerializeObject(user.UserName));
                    return Redirect("../Order/Index");
                }
            }
            return View();
        }
    }
}
