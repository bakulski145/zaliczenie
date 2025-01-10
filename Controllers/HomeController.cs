using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace zaliczenie.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var email = HttpContext.Session.GetString("UserEmail");

            if (!string.IsNullOrEmpty(email))
            {
                ViewBag.Username = email;
            }


            return View();
        }

    }
}
