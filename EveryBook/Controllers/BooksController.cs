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
    public class BooksController : Controller
    {
        private readonly EveryBookContext _context;

        public BooksController(EveryBookContext context)
        {
            _context = context;
        }

        // GET: Books?bookname=BLA
        public async Task<IActionResult> Index([FromQuery] string bookName, [FromQuery] string? genreName, [FromQuery] float? price)
        {
            IEnumerable<Book> everyBookContext = _context.Book.Include(b => b.Genre);

            if (!String.IsNullOrEmpty(bookName))
            {
                everyBookContext = everyBookContext.Where(b => b.Name.Contains(bookName)).ToList();
            }

            if (!String.IsNullOrEmpty(genreName))
            {
                everyBookContext = everyBookContext.Where(b => b.Genre.Name == genreName).ToList();
            }

            if (price.HasValue)
            {
                everyBookContext = everyBookContext.Where(b => b.Price <= price).ToList();
            }

            return View(everyBookContext);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Genre.Where(g => g.IsDeleted == false), "Id", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PictureUrl,Name,Author,OriginalPrice,Price,Description,AvailableQuantity,IsDeleted,GenreId")] Book book)
        {
            if (ModelState.IsValid && book.AvailableQuantity > 0 && book.OriginalPrice > 0 && book.Price > book.OriginalPrice && (book.PictureUrl != null || book.PictureUrl != ""))
            {
                var isalreadyCreated = _context.Book.Where(b => b.Name.ToLower() == book.Name.ToLower() && b.Author.ToLower() == book.Author.ToLower() && b.GenreId == book.GenreId).FirstOrDefault();
                if (isalreadyCreated != null)
                {
                    if (isalreadyCreated.IsDeleted == false)
                    {
                        return View(book);
                    }
                    isalreadyCreated.IsDeleted = false;
                    _context.Update(isalreadyCreated);
                }
                else
                {
                    _context.Add(book);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            if (book.Price < book.OriginalPrice)
            {
                ModelState.AddModelError(nameof(book.Price), "Price needed to be grater than the \"Original Price\".");
            }
            if (book.PictureUrl == null || book.PictureUrl == "")
            {
                ModelState.AddModelError(nameof(book.PictureUrl), "The Picture Url field is required.");
            }
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Name", book.GenreId);
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Name", book.GenreId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,PictureUrl,Name,Author,OriginalPrice,Price,Description,AvailableQuantity,GenreId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Name", book.GenreId);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var book = await _context.Book.FindAsync(id);
            book.IsDeleted = true;
            _context.Book.Update(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(long id)
        {
            return _context.Book.Any(e => e.Id == id);
        }

        //For Statistics

        // GET: Books/BooksByGenre
        [HttpGet]
        public IEnumerable BooksByGenre()
        {
            var booksByGenre = _context.Book
                .Where(b => b.IsDeleted == false)
                .GroupBy(b => new { GenreName = b.Genre.Name })
                .Select(r => new { Value = r.Count(), Name = r.Key.GenreName })
                .ToList();
            return booksByGenre;
        }

        //join - Books in Genre
        [HttpGet]
        public IEnumerable BooksInGenre()
        {
            var BooksInGenre = (from b in _context.Book
                                where b.IsDeleted == false
                                join g in _context.Genre on b.GenreId equals g.Id
                                where g.IsDeleted == false
                                group g by g.Name into bgName
                                select new { Value = bgName.Count(), Name = bgName.Key }).ToList();
            return BooksInGenre;
        }

        //join - most purchased Genre
        [HttpGet]
        public IEnumerable MostPurGenre()
        {
            var MostPurGenre = (from o in _context.Order
                                join b in _context.Book on false equals b.IsDeleted
                                join g in _context.Genre on b.GenreId equals g.Id
                                where g.IsDeleted == false
                                where o.Books.Contains(b)
                                group g by g.Name into bgName
                                select new { Value = bgName.Count(), Name = bgName.Key }).ToList();
            return MostPurGenre;
        }

        //join - most purchased Book
        [HttpGet]
        public IEnumerable MostPurBook()
        {
            var MostPurBook = (from o in _context.Order
                               join b in _context.Book on false equals b.IsDeleted
                               where o.Books.Contains(b)
                               group b by b.Name into bo
                               select new { Value = bo.Count(), Name = bo.Key }).ToList();
            return MostPurBook;
        }
    }
}
