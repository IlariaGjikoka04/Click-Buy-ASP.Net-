using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Click_Buy1.Data;
using Click_Buy1.Models;
using Microsoft.AspNetCore.Authorization;

namespace Click_Buy1.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CheckoutController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Checkout
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                TempData["ErrorMessage"] = "Administrators cannot access the checkout page.";
                return RedirectToAction("Index", "Home"); // Ridrejto në faqen kryesore ose diku tjetër
            }
            return _context.AddToCart != null ? 
                          View(await _context.AddToCart.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.AddToCart'  is null.");
        }
       
        // GET: Checkout/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AddToCart == null)
            {
                return NotFound();
            }

            var addToCart = await _context.AddToCart
                .FirstOrDefaultAsync(m => m.Id == id);
            if (addToCart == null)
            {
                return NotFound();
            }

            return View(addToCart);
        }
     

        // GET: Checkout/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AddToCart == null)
            {
                return NotFound();
            }

            var addToCart = await _context.AddToCart.FindAsync(id);
            if (addToCart == null)
            {
                return NotFound();
            }
            return View(addToCart);
        }

        // POST: Checkout/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImageUrl,Price,Quantity,CategoryId")] AddToCart addToCart)
        {
            if (id != addToCart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(addToCart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddToCartExists(addToCart.Id))
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
            return View(addToCart);
        }

        // GET: Checkout/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AddToCart == null)
            {
                return NotFound();
            }

            var addToCart = await _context.AddToCart
                .FirstOrDefaultAsync(m => m.Id == id);
            if (addToCart == null)
            {
                return NotFound();
            }

            return View(addToCart);
        }

        // POST: Checkout/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AddToCart == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AddToCart'  is null.");
            }
            var addToCart = await _context.AddToCart.FindAsync(id);
            if (addToCart != null)
            {
                _context.AddToCart.Remove(addToCart);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddToCartExists(int id)
        {
          return (_context.AddToCart?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        [HttpPost]
        public IActionResult CompleteOrder([FromBody] Orders model)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                return Json(new { success = false, message = "Email is required." });
            }

            // Kontrollo nëse email-i ekziston në tabelën AspNetUsers
            var userExists = _context.Users.Any(u => u.Email == model.Email);
            if (!userExists)
            {
                return Json(new { success = false, message = "Email not found. Please register first." });
            }

            // Llogarisim totalin nga produktet në shportë
            decimal totalAmount = _context.AddToCart.Sum(item => item.Price * item.Quantity);

            // Shtojmë porosinë në databazë
            var newOrder = new Orders
            {
                Email = model.Email,
                TotalAmount = totalAmount
            };

            _context.Orders.Add(newOrder);
            _context.SaveChanges();

            // Fshijmë shportën pas përfundimit të porosisë
            _context.AddToCart.RemoveRange(_context.AddToCart);
            _context.SaveChanges();

            return Json(new { success = true });
        }
    }
}
