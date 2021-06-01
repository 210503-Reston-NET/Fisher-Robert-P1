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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
