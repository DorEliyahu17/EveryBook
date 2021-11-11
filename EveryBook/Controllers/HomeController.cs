using EveryBook.Data;
using EveryBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using EveryBook.Controllers;
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
            var mostPopularBooks = (from o in _context.Order
                                    join b in _context.Book on false equals b.IsDeleted
                                    where o.Books.Contains(b)
                                    group b by b.Name into bo
                                    select new { Value = bo.Count(), Name = bo.Key }).ToArray(); 
            if(mostPopularBooks.Length > 0)
            {
                ViewData["FirstBookName"] = mostPopularBooks[0].Name;
                ViewData["FirstBookValue"] = mostPopularBooks[0].Value;
                if (mostPopularBooks.Length > 1)
                {
                    ViewData["SecondBookName"] = mostPopularBooks[1].Name;
                    ViewData["SecondBookValue"] = mostPopularBooks[1].Value;
                    if (mostPopularBooks.Length > 2)
                    {
                        ViewData["ThirdBookName"] = mostPopularBooks[2].Name;
                        ViewData["ThirdBookValue"] = mostPopularBooks[2].Value;
                        if (mostPopularBooks.Length > 3)
                        {
                            ViewData["ForthBookName"] = mostPopularBooks[3].Name;
                            ViewData["ForthBookValue"] = mostPopularBooks[3].Value;
                            if (mostPopularBooks.Length > 4)
                            {
                                ViewData["FifthBookName"] = mostPopularBooks[4].Name;
                                ViewData["FifthBookValue"] = mostPopularBooks[4].Value;
                            }
                        }
                    }
                }
            }
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

        // GET: Home/UsersList
        public IActionResult UsersList()
        {
            if (_SignInManager.IsSignedIn(User))
            {
                if (_UserManager.IsInRoleAsync(_UserManager.GetUserAsync(User).Result, "Admin").Result)
                {
                    IEnumerable<ExtendUser> everyBookContext = _context.ExtendUser;

                    return View(everyBookContext);
                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        // POST: Home/ChangeRole/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeRole(string id, [Bind("Id")] ExtendUser extendUser)
        {
            var user = await _UserManager.FindByIdAsync(id);
            var isAdmin = await _UserManager.IsInRoleAsync(user, "Admin");
            if (isAdmin)
            {
                await _UserManager.RemoveFromRoleAsync(user, "Admin");
                await _UserManager.AddToRoleAsync(user, "User");
            } else
            {
                await _UserManager.RemoveFromRoleAsync(user, "User");
                await _UserManager.AddToRoleAsync(user, "Admin");
            }
            _context.Update(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(UsersList));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
