using Group7_FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Group7_FinalProject.Controllers
{
    public class HomeController : Controller
    {

        private Repository<Product> data { get; set; }
        public HomeController(FashionstoreContext ctx) => data = new Repository<Product>(ctx);

        public ViewResult Index()
        {
            var random = data.Get(new QueryOptions<Product>
            {
                OrderBy = b => Guid.NewGuid()
            });

            return View(random);
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
