using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace zaliczenie.Controllers
{
    public class HomeController : Controller
    {
        // Strona g³ówna
        public IActionResult Index()
        {
            // Sprawdzamy, czy u¿ytkownik jest zalogowany
            var username = HttpContext.Session.GetString("Username");

            // Jeœli u¿ytkownik jest zalogowany, przekazujemy nazwê u¿ytkownika do widoku
            if (!string.IsNullOrEmpty(username))
            {
                ViewBag.Username = username; // Przechowujemy dane w ViewBag
            }

            return View();
        }
    }
}
