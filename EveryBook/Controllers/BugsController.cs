using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
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
    public class BugsController : Controller
    {
        private readonly EveryBookContext _context;
        private readonly UserManager<ExtendUser> _UserManager;

        public BugsController(EveryBookContext context, UserManager<ExtendUser> UserManager)
        {
            _context = context;
            _UserManager = UserManager;
        }

        // GET: Bugs
        public async Task<IActionResult> Index()
        {
            var everyBookContext = _context.Bug.Include(b => b.ExtendUser);
            return View(await everyBookContext.ToListAsync());
        }

        // GET: Bugs/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bug = await _context.Bug
                .Include(b => b.ExtendUser)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (bug == null)
            {
                return NotFound();
            }

            return View(bug);
        }

        // GET: Bugs/Create
        public IActionResult Create()
        {
            ViewData["ExtendUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Bugs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,IsDone,ExtendUserId")] Bug bug)
        {
            var user = _UserManager.Users.Where(u => u.Email == User.Identity.Name).ToList();
            bug.ExtendUserId = user[0].Id;
            bug.IsDone = false;
            if (ModelState.IsValid)
            {
                _context.Add(bug);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExtendUserId"] = new SelectList(_context.Users, "Id", "Id", bug.ExtendUserId);
            return View(bug);
        }

        
        // GET: Bugs/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bug = await _context.Bug.FindAsync(id);
            if (bug == null)
            {
                return NotFound();
            }
            ViewData["ExtendUserId"] = new SelectList(_context.Users, "Id", "Id", bug.ExtendUserId);
            return View(bug);
        }

        // POST: Bugs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Title,Description,IsDone,ExtendUserId")] Bug bug)
        {
            if (id != bug.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bug);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BugExists(bug.Id))
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
            ViewData["ExtendUserId"] = new SelectList(_context.Users, "Id", "Id", bug.ExtendUserId);
            return View(bug);
        }

        // POST: Bugs/ChangeDone/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeDone(long id, [Bind("Id,Title,Description,IsDone,ExtendUserId")] Bug bug)
        {
            if (id != bug.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bug);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BugExists(bug.Id))
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
            ViewData["ExtendUserId"] = new SelectList(_context.Users, "Id", "Id", bug.ExtendUserId);
            return View(bug);
        }

        // GET: Bugs/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bug = await _context.Bug
                .Include(b => b.ExtendUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bug == null)
            {
                return NotFound();
            }

            return View(bug);
        }

        // POST: Bugs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var bug = await _context.Bug.FindAsync(id);
            _context.Bug.Remove(bug);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BugExists(long id)
        {
            return _context.Bug.Any(e => e.Id == id);
        }

        //most reporter User
        [HttpGet]
        public IEnumerable CountIsDone()
        {
            var CountIsDone = (from b in _context.Bug
                               group b by b.IsDone into bd
                               select new { Value = bd.Count(), Name = ( bd.Key == true ? "Done" : "Pending" ) }).ToList();
            return CountIsDone;
        }
    }
}
