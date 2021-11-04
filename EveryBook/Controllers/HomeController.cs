using EveryBook.Data;
using EveryBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EveryBook.Controllers
{
    public class HomeController : Controller
    {
        private readonly EveryBookContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, EveryBookContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            //return Error();
            return View();
        }

        public IActionResult About()
        {
            IEnumerable<DistributionUnit> everyBookContext = _context.DistributionUnit.Include(s => s.Location);

            return View(everyBookContext);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
