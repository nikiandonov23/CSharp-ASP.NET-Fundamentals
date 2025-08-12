using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingListApp2025.Data;
using ShoppingListApp2025.Models;
using System.Diagnostics;

namespace ShoppingListApp2025.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShoppingList2025DbContext _context;

        public HomeController(ILogger<HomeController> logger, ShoppingList2025DbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }




        public IActionResult Products()
        {
            var products = _context.Products.ToList();
            return View(products);
        }



        public IActionResult ProductNotes(int productId)
        {
            var product = _context.Products
                .Include(x => x.ProductNotes)
                .FirstOrDefault(x => x.Id == productId);

            if (product == null)
            {
                return NotFound();
            }

            return View("ViewNotes", product); // ✅ Подаваме модела и посочваме правилно View-то

        }





        public IActionResult ViewNotes(int id)
        {
            var product = _context.Products
                .Include(p => p.ProductNotes)
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product); // ТУК подаваме модела, а не product.Name!
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
