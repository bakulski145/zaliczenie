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

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Istnieje już użytkownik o tym emailu.");
                    return View(user);
                }

                if (_context.Users.Any(u => u.Username == user.Username))
                {
                    ModelState.AddModelError("Username", "Istnieje już użytkownik o tej nazwie.");
                    return View(user);
                }

                _context.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users
                    .FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

                if (user != null)
                {

                    HttpContext.Session.SetString("UserEmail", user.Email);
                    HttpContext.Session.SetString("UserRole", user.Role); 

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
            if (HttpContext.Session.GetString("UserRole") == null)
            {
                return RedirectToAction("Login");
            }

            var email = HttpContext.Session.GetString("UserEmail");
            var role = HttpContext.Session.GetString("UserRole");

            var model = new UserProfileViewModel
            {
                Email = email,
                Role = role
            };

            return View(model);
        }



        public IActionResult Logout()
{
    HttpContext.Session.Remove("UserEmail");
    HttpContext.Session.Remove("UserRole");

    TempData["SuccessMessage"] = "Wylogowano!";
    
    return RedirectToAction("Index", "Home");
}
    }
}