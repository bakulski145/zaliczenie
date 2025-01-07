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
            var username = HttpContext.Session.GetString("Username");

            // Je�li u�ytkownik jest zalogowany, przekazujemy nazw� u�ytkownika do widoku
            if (!string.IsNullOrEmpty(username))
            {
                ViewBag.Username = username; // Przechowujemy dane w ViewBag
            }

            return View();
        }
    }
}
