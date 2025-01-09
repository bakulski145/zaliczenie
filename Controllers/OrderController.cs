using Microsoft.AspNetCore.Mvc;
using zaliczenie.Data;
using zaliczenie.Models;

public class OrderController : Controller
{
    private readonly ApplicationDbContext _context;

    public OrderController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(int productId, int quantity)
    {
        var userEmail = HttpContext.Session.GetString("UserEmail");
        ViewBag.UserEmail = userEmail;

        var order = new Order
        {
            ProductId = productId,
            Quantity = quantity,
            OrderDate = DateTime.Now,
            UserEmail = userEmail
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Product");
    }
}
