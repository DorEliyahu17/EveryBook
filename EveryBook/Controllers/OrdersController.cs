using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EveryBook.Data;
using EveryBook.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Collections;
using Microsoft.AspNetCore.Identity;

namespace EveryBook.Controllers
{
    public class OrdersController : Controller
    {
        private readonly EveryBookContext _context;
        private readonly SignInManager<ExtendUser> _SignInManager;
        private readonly UserManager<ExtendUser> _UserManager;

        public OrdersController(EveryBookContext context, SignInManager<ExtendUser> SignInManager, UserManager<ExtendUser> UserManager)
        {
            _context = context;
            _SignInManager = SignInManager;
            _UserManager = UserManager;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var loggedInUser = _UserManager.GetUserAsync(User).Result.Id;
            var orders = _context.Order.Include(o => o.DistributionUnit).Include(o => o.ExtendUser);
            if (_UserManager.IsInRoleAsync(_UserManager.GetUserAsync(User).Result, "User").Result)
            {
                var everyBookContext = orders.Where(o => o.ExtendUserId == loggedInUser);
                return View(await everyBookContext.ToListAsync());
            }
            return View(await orders.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.DistributionUnit)
                .Include(o => o.ExtendUser)
                .Include(o => o.Books)
                .FirstOrDefaultAsync(m => m.Id == id);
            List<Book> booksinOrder = new List<Book>();
            for (int i = 0; i < order.Books.Count; i++)
            {
                booksinOrder.Add(_context.Book.Include(b => b.Genre).Where(b => b.Id == order.Books[i].Id).FirstOrDefault());
            }
            order.Books = booksinOrder;
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        private string GetUniqueSessionKey(string key)
        {
            return HttpContext.User.Identity.Name.ToString() + key;
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["DistributionUnitId"] = new SelectList(_context.DistributionUnit, "Id", "Name");
            ViewData["ExtendUserId"] = new SelectList(_context.ExtendUser, "Id", "Id");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExtendUserId,DistributionUnitId")] Order order)
        {
            if (ModelState.IsValid)
            {
                var booksInOrder = JsonConvert.DeserializeObject<List<Book>>(HttpContext.Session.GetString(GetUniqueSessionKey("BooksInCart")));
                List<Book> books = new List<Book>();
                for(int i = 0; i < booksInOrder.Count; i++)
                {
                    books.Add(_context.Book.Include(b => b.Genre).Where(b => b.Id == booksInOrder[i].Id).FirstOrDefault());
                }
                order.Books = books;
                order.DistributionUnit = _context.DistributionUnit.Where(d => d.Id == order.DistributionUnitId).FirstOrDefault();
                _context.Add(order);
                await _context.SaveChangesAsync();
                HttpContext.Session.SetString(GetUniqueSessionKey("BooksInCart"), JsonConvert.SerializeObject(""));
                HttpContext.Session.SetInt32(GetUniqueSessionKey("NumOfBooksInCart"), 0);
                HttpContext.Session.SetInt32(GetUniqueSessionKey("TotalToPay"), 0);
                return RedirectToAction(nameof(Index));
            }
            ViewData["DistributionUnitId"] = new SelectList(_context.DistributionUnit, "Id", "Id", order.DistributionUnitId);
            ViewData["ExtendUserId"] = new SelectList(_context.ExtendUser, "Id", "Id", order.ExtendUserId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["DistributionUnitId"] = new SelectList(_context.DistributionUnit, "Id", "Id", order.DistributionUnitId);
            ViewData["ExtendUserId"] = new SelectList(_context.ExtendUser, "Id", "Id", order.ExtendUserId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,PurchaseTime,ExtendUserId,DistributionUnitId")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DistributionUnitId"] = new SelectList(_context.DistributionUnit, "Id", "Id", order.DistributionUnitId);
            ViewData["ExtendUserId"] = new SelectList(_context.ExtendUser, "Id", "Id", order.ExtendUserId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.DistributionUnit)
                .Include(o => o.ExtendUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(long id)
        {
            return _context.Order.Any(e => e.Id == id);
        }

        //join - most wanted DistributionUnit
        [HttpGet]
        public IEnumerable MostWantedDS()
        {
            var MostWantedDS = (from o in _context.Order
                             join ods in _context.DistributionUnit on o.DistributionUnitId equals ods.Id
                             group ods by ods.Name into dsn
                             select new { Value = dsn.Count(), Name = dsn.Key }).ToList();
            return MostWantedDS;
        }
    }
}
