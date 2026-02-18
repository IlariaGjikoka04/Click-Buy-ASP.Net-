using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Click_Buy1.Data;
using Click_Buy1.Models;

namespace Click_Buy1.Controllers
{
    [Authorize(Roles="Admin")]
    public class JeansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JeansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Jeans
        public async Task<IActionResult> Index()
        {
              return _context.Jeans != null ? 
                          View(await _context.Jeans.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Jeans'  is null.");
        }

        // GET: Jeans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Jeans == null)
            {
                return NotFound();
            }

            var jeans = await _context.Jeans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jeans == null)
            {
                return NotFound();
            }

            return View(jeans);
        }

        // GET: Jeans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jeans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ImageUrl,Price,CategoryId")] Jeans jeans)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jeans);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jeans);
        }

        // GET: Jeans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Jeans == null)
            {
                return NotFound();
            }

            var jeans = await _context.Jeans.FindAsync(id);
            if (jeans == null)
            {
                return NotFound();
            }
            return View(jeans);
        }

        // POST: Jeans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImageUrl,Price,CategoryId")] Jeans jeans)
        {
            if (id != jeans.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jeans);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JeansExists(jeans.Id))
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
            return View(jeans);
        }

        // GET: Jeans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Jeans == null)
            {
                return NotFound();
            }

            var jeans = await _context.Jeans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jeans == null)
            {
                return NotFound();
            }

            return View(jeans);
        }

        // POST: Jeans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Jeans == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Jeans'  is null.");
            }
            var jeans = await _context.Jeans.FindAsync(id);
            if (jeans != null)
            {
                _context.Jeans.Remove(jeans);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JeansExists(int id)
        {
          return (_context.Jeans?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
