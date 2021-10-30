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
            IEnumerable<Store> everyBookContext = _context.Store.Include(s => s.Location);
            /*
            List<Store> model = new List<Store>();
            var locations = new List<Location>()
            {
                new Locations(1, "Ilan Malisov - ilan07m@gmail.com","Herzliya, israel", 32.163510, 34.842670),
                new Locations(2, "Michael Tsiriulinikov - misha1235000@gmail.com","Bat Yam, israel", 32.011850, 34.745310),
                new Locations(3, "Ofek Reuveni - ofekrv@gmail.com","Rishon Lezion, israel", 31.963760, 34.815720),
                new Locations(3, "Colman Collage of management - 03-963-4000","Rishon Lezion, israel", 31.986460, 34.806020)
            };
            model.LocationList = locations;
            */
            return View(everyBookContext);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
