using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using zaliczenie.Data;
using zaliczenie.Models;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;


namespace zaliczenie.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context; // Upewnij się, że kontekst jest poprawnie wstrzykiwany
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


        // Akcja, która dodaje produkt do koszyka
        [HttpPost]
            public IActionResult AddToCart(int productId)
            {
                var product = _context.Products.FirstOrDefault(p => p.Id == productId);
                if (product == null)
                {
                    return NotFound();
                }

                // Pobierz koszyk z sesji lub utwórz nowy
                var cart = GetCart();

                // Sprawdź, czy produkt już istnieje w koszyku
                var cartItem = cart.FirstOrDefault(c => c.ProductId == productId);
                if (cartItem == null)
                {
                    // Dodaj nowy produkt do koszyka
                    cart.Add(new CartItem
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        Quantity = 1,
                        Price = product.Price
                    });
                }
                else
                {
                    // Zwiększ ilość, jeśli produkt już istnieje
                    cartItem.Quantity++;
                }

                // Zapisz koszyk w sesji
                SaveCart(cart);

                return RedirectToAction("Index");
            }

            // Akcja wyświetlająca zawartość koszyka
            public IActionResult Cart()
            {
                var cart = GetCart();
                return View(cart);
            }

            // Pomocnicze metody
            private List<CartItem> GetCart()
            {
                var cartJson = HttpContext.Session.GetString("Cart");
                return string.IsNullOrEmpty(cartJson) ? new List<CartItem>() : JsonConvert.DeserializeObject<List<CartItem>>(cartJson);
            }

            private void SaveCart(List<CartItem> cart)
            {
                var cartJson = JsonConvert.SerializeObject(cart);
                HttpContext.Session.SetString("Cart", cartJson);
            }
        }
    }

