using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EveryBook.Data;
using EveryBook.Models;
using System.Collections;

namespace EveryBook.Controllers
{
    public class OrdersController : Controller
    {
        private readonly EveryBookContext _context;

        public OrdersController(EveryBookContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var everyBookContext = _context.Order.Include(o => o.DistributionUnit).Include(o => o.ExtendUser);
            return View(await everyBookContext.ToListAsync());
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
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
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
        public async Task<IActionResult> Create([Bind("Id,PurchaseTime,ExtendUserId,DistributionUnitId")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
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
