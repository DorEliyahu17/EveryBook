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
using Microsoft.AspNetCore.Identity;

namespace EveryBook.Controllers
{
    public class HomeController : Controller
    {
        private readonly EveryBookContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<ExtendUser> _SignInManager;
        private readonly UserManager<ExtendUser> _UserManager;

        public HomeController(ILogger<HomeController> logger, EveryBookContext context, SignInManager<ExtendUser> SignInManager, UserManager<ExtendUser> UserManager)
        {
            _context = context;
            _logger = logger;
            _SignInManager = SignInManager;
            _UserManager = UserManager;
        }

        // GET: /
        public IActionResult Index()
        {
            //return Error();
            return View();
        }

        // GET: Home/About
        public IActionResult About()
        {
            IEnumerable<DistributionUnit> everyBookContext = _context.DistributionUnit.Include(s => s.Location);

            return View(everyBookContext);
        }

        // GET: Home/Statistics
        public IActionResult Statistics()
        {
            if (!(_SignInManager.IsSignedIn(User) && _UserManager.IsInRoleAsync(_UserManager.GetUserAsync(User).Result, "Admin").Result))
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
