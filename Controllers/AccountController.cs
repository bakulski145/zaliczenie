using Microsoft.AspNetCore.Mvc;
using zaliczenie.Data;
using zaliczenie.Models;
using Microsoft.AspNetCore.Http;

namespace zaliczenie.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Formularz rejestracji (GET)
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Formularz logowania (GET)
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Rejestracja użytkownika (POST)
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Sprawdzamy, czy e-mail już istnieje w bazie danych
                if (_context.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "This email is already registered.");
                    return View(user);
                }

                // Sprawdzamy, czy nazwa użytkownika już istnieje w bazie danych
                if (_context.Users.Any(u => u.Username == user.Username))
                {
                    ModelState.AddModelError("Username", "This username is already taken.");
                    return View(user);
                }

                _context.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

        // Logowanie użytkownika (POST)
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                ModelState.AddModelError("Username", "Username not found.");
                return View(); // Zwróci widok z błędami
            }

            if (user.Password != password)
            {
                ModelState.AddModelError("Password", "Incorrect password.");
                return View(); // Zwróci widok z błędami
            }

            // Zapisanie sesji
            HttpContext.Session.SetString("Username", user.Username);

            // Komunikat po udanym logowaniu
            TempData["SuccessMessage"] = "Logged in successfully!";
            return RedirectToAction("Index", "Home");
        }


        // Wylogowanie użytkownika
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Username");
            TempData["SuccessMessage"] = "Logged out successfully!";
            return RedirectToAction("Index", "Home");
        }
    }
}
