using Click_Buy1.Data;
using Click_Buy1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Click_Buy1.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddToCart([FromBody] AddToCart model)
        {
            if (model == null)
            {
                return Json(new { success = false, message = "Invalid product data." });
            }
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                return Json(new { success = false, message = "Administrators cannot perform this action." });
            }
            // Kontrollo nëse produkti ekziston në shportë
            var existingItem = _context.AddToCart.FirstOrDefault(c => c.Name == model.Name);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                // Shto produktin e ri
                _context.AddToCart.Add(new AddToCart
                {
                    Name = model.Name,
                    ImageUrl = model.ImageUrl,
                    Price = model.Price,
                    Quantity = 1,
                    CategoryId = model.CategoryId
                });
            }

            _context.SaveChanges();
            return Json(new { success = true, message = $"{model.Name} added to basket." });
        }

        [HttpGet]
        public IActionResult GetCartItems()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                return Json(new { success = false, message = "Administrators cannot view cart items." });
            }

            var cartItems = _context.AddToCart.Select(c => new
            {
                c.Name,
                c.ImageUrl,
                c.Price,
                c.Quantity
            }).ToList();

            return Json(cartItems);
        }
        public IActionResult Jackets()
        {
            // Merr vetëm produktet që janë pjesë e kategorisë "Jackets"
            var jackets = _context.Jackets
                       .Where(jk => jk.CategoryId == 7)
                       .ToList();

            return View(jackets);
        }
        public IActionResult Dresses()
        {
            // Merr vetëm produktet që janë pjesë e kategorisë "Dresses"
            var dresses = _context.Products
                       .Where(p => p.CategoryId == 1)
                       .ToList();

            return View(dresses);
        }
        public IActionResult Jeans()
        {
            // Merr vetëm produktet që janë pjesë e kategorisë "Dresses"
            var jeans= _context.Jeans
                       .Where(j => j.CategoryId == 2)
                       .ToList();

            return View(jeans);
        }
        public IActionResult SweatersANDcardigans()
        {
            // Merr vetëm produktet që janë pjesë e kategorisë "Sweaters"
            var sweaters = _context.SweatersANDcardigans
                       .Where(s => s.CategoryId == 3)
                       .ToList();

            return View(sweaters);
        }
        public IActionResult Trousers()
        {
            // Merr vetëm produktet që janë pjesë e kategorisë "Sweaters"
            var trousers = _context.Trousers
                       .Where(t => t.CategoryId == 4)
                       .ToList();

            return View(trousers);
        }
        public IActionResult Tshirts()
        {
            // Merr vetëm produktet që janë pjesë e kategorisë "Sweaters"
            var tshirts = _context.Tshirts
                       .Where(ts => ts.CategoryId == 5)
                       .ToList();

            return View(tshirts);
        }
        public IActionResult Payment()
        {
            return View();
        }
        public IActionResult NewCollection()
        {
            // Merr produktin me Id = 1
            var collection1 = _context.NewCollection.FirstOrDefault(c => c.Id == 1);

            if (collection1 == null)
            {
                return NotFound();
            }

            return View(collection1);
        }
        public IActionResult NewCollection2()
        {
            // Merr produktin me Id = 1
            var collection2 = _context.NewCollection.FirstOrDefault(c => c.Id == 2);

            if (collection2 == null)
            {
                return NotFound();
            }

            return View(collection2);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
