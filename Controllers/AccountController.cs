using Microsoft.AspNetCore.Mvc;
using zaliczenie.Data;
using zaliczenie.Models;
using Microsoft.AspNetCore.Http;
using zaliczenie.ViewModels;


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
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users
                    .FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

                if (user != null)
                {
                    // Zapisz dane do sesji
                    HttpContext.Session.SetString("UserEmail", user.Email);
                    HttpContext.Session.SetString("UserRole", user.Role);  // Dodajemy również rolę

                    // Przekierowanie po zalogowaniu
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Nieprawidłowy email lub hasło.");
                }
            }

            return View(model);
        }



        public IActionResult UserProfile()
        {
            // Sprawdź, czy użytkownik jest zalogowany
            if (HttpContext.Session.GetString("UserRole") == null)
            {
                return RedirectToAction("Login");
            }

            // Pobierz dane użytkownika z sesji (lub bazy danych)
            var email = HttpContext.Session.GetString("UserEmail");
            var role = HttpContext.Session.GetString("UserRole");

            // Utwórz model dla widoku
            var model = new UserProfileViewModel
            {
                Email = email,
                Role = role
            };

            return View(model);
        }





        // Wylogowanie użytkownika
        public IActionResult Logout()
        {
            // Usunięcie danych użytkownika z sesji
            HttpContext.Session.Remove("UserEmail");
            HttpContext.Session.Remove("UserRole");

            // Ustawienie komunikatu o wylogowaniu
            TempData["SuccessMessage"] = "Logged out successfully!";

            // Przekierowanie na stronę główną
            return RedirectToAction("Index", "Home");
        }

    }
}