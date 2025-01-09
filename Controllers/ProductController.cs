using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using zaliczenie.Data;
using zaliczenie.Models;
using System.Linq;
using Newtonsoft.Json;

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
            ViewBag.UserRole = userRole;

            var userEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserEmail = userEmail;

            var products = _context.Products.ToList();
            return View(products);
        }
        public IActionResult Orders()
        {
            var orders = _context.Orders.ToList();

            return View(orders);
        }

        public IActionResult Create()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Admin")
            {
                return Unauthorized();
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

