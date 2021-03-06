using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EveryBook.Data;
using EveryBook.Models;

namespace EveryBook.Controllers
{
    public class GenresController : Controller
    {
        private readonly EveryBookContext _context;

        public GenresController(EveryBookContext context)
        {
            _context = context;
        }

        // GET: Genres
        public async Task<IActionResult> Index([FromQuery] string? genreName)
        {
            IEnumerable<Genre> everyBookContext = _context.Genre;

            if (!String.IsNullOrEmpty(genreName))
            {
                everyBookContext = everyBookContext.Where(g => g.Name.ToLower().Contains(genreName.ToLower())).ToList();
            }

            return View(everyBookContext);
        }

        // GET: Genres/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = await _context.Genre
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        // GET: Genres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IsDeleted")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                var isalreadyCreated = _context.Genre.Where(g => g.Name.ToLower() == genre.Name.ToLower()).FirstOrDefault();
                if (isalreadyCreated != null)
                {
                    if (isalreadyCreated.IsDeleted == false)
                    {
                        return View(genre);
                    }
                    isalreadyCreated.IsDeleted = false;
                    _context.Update(isalreadyCreated);
                } else
                {
                    _context.Add(genre);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        // GET: Genres/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = await _context.Genre.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }

        // POST: Genres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,IsDeleted")] Genre genre)
        {
            if (id != genre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(genre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenreExists(genre.Id))
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
            return View(genre);
        }

        // GET: Genres/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = await _context.Genre
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var genre = await _context.Genre.FindAsync(id);
            genre.IsDeleted = true;
            var books = _context.Book.Where(b => b.GenreId == id).ToArray();
            for (int i = 0; i < books.Length; i++)
            {
                books[i].IsDeleted = true;
                _context.Book.Update(books[i]);
            }
            _context.Genre.Update(genre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenreExists(long id)
        {
            return _context.Genre.Any(e => e.Id == id);
        }
    }
}
