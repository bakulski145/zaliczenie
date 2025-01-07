using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace zaliczenie.Controllers
{
    public class HomeController : Controller
    {
        // Strona g��wna
        public IActionResult Index()
        {
            // Sprawdzamy, czy u�ytkownik jest zalogowany
            var email = HttpContext.Session.GetString("UserEmail");

            // Je�li u�ytkownik jest zalogowany, przekazujemy nazw� u�ytkownika do widoku
            if (!string.IsNullOrEmpty(email))
            {
                ViewBag.Username = email; // Przechowujemy email w ViewBag
            }

            return View();
        }

    }
}
