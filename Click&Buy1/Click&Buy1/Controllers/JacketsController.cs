using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Click_Buy1.Data;
using Click_Buy1.Models;

namespace Click_Buy1.Controllers
{
    public class JacketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JacketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Jackets
        public async Task<IActionResult> Index()
        {
              return _context.Jackets != null ? 
                          View(await _context.Jackets.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Jackets'  is null.");
        }

        // GET: Jackets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Jackets == null)
            {
                return NotFound();
            }

            var jackets = await _context.Jackets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jackets == null)
            {
                return NotFound();
            }

            return View(jackets);
        }

        // GET: Jackets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jackets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ImageUrl,Price,CategoryId")] Jackets jackets)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jackets);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jackets);
        }

        // GET: Jackets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Jackets == null)
            {
                return NotFound();
            }

            var jackets = await _context.Jackets.FindAsync(id);
            if (jackets == null)
            {
                return NotFound();
            }
            return View(jackets);
        }

        // POST: Jackets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImageUrl,Price,CategoryId")] Jackets jackets)
        {
            if (id != jackets.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jackets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JacketsExists(jackets.Id))
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
            return View(jackets);
        }

        // GET: Jackets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Jackets == null)
            {
                return NotFound();
            }

            var jackets = await _context.Jackets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jackets == null)
            {
                return NotFound();
            }

            return View(jackets);
        }

        // POST: Jackets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Jackets == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Jackets'  is null.");
            }
            var jackets = await _context.Jackets.FindAsync(id);
            if (jackets != null)
            {
                _context.Jackets.Remove(jackets);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JacketsExists(int id)
        {
          return (_context.Jackets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
