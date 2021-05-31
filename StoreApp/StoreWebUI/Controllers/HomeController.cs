using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StoreWebUI.Models;
using StoreModels;
using StoreBL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace StoreWebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private StoreBLInterface _BL;

        public HomeController(ILogger<HomeController> logger, StoreBLInterface storeBussinessLayer)
        {
            _logger = logger;
            _BL = storeBussinessLayer;
        }

        public IActionResult Index()
        {
            return View();
        }
        public ActionResult VerifyLogin(UserVM given)
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
                    return Redirect("../Home/Index");
                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
