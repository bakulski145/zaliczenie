using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using zaliczenie.Data;
using zaliczenie.Models;
using System.Linq;

namespace zaliczenie.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            ViewBag.UserRole = userRole; // Przekazanie roli do widoku

            var products = _context.Products.ToList();
            return View(products);
        }


        // Akcja do wyświetlania formularza dodawania produktu
        public IActionResult Create()
        {
            var userRole = HttpContext.Session.GetString("UserRole"); // Pobierz rolę z sesji
            if (userRole != "Admin")
            {
                return Unauthorized(); // Jeśli użytkownik nie jest adminem, zwróć błąd 401
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Admin")
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }
    }
}
