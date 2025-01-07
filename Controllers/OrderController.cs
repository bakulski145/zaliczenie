using Microsoft.AspNetCore.Mvc;
using zaliczenie.Models;

namespace zaliczenie.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult CreateOrder()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateOrder(Order order)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
