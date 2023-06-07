using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UpMoneyProjesi.Models;

namespace UpMoneyProjesi.Controllers
{
    public class HomeController : Controller
    {

        private readonly WalletContext _context;

        public HomeController(WalletContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            string member = HttpContext.Session.GetString("customer");
            var list = _context.Customers.Where(x => x.CustomerId.ToString() == member);
            foreach (var item in list)
            {
                ViewData["Message"] = item.CustomerName;

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
