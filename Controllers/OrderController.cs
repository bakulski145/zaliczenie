using Microsoft.AspNetCore.Mvc;
using zaliczenie.Data;
using zaliczenie.Models;
using Microsoft.EntityFrameworkCore;


public class OrderController : Controller
{
    // Deklaracja DbContext
    private readonly ApplicationDbContext _dbContext;

    // Konstruktor kontrolera
    public OrderController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext; // Inicjalizacja DbContext
    }

    // Przykładowa akcja tworzenia zamówienia
    public IActionResult CreateOrder(Order order)
    {
        try
        {
            _dbContext.Orders.Add(order); // Dodaj zamówienie do DbContext
            _dbContext.SaveChanges(); // Zapisz zmiany do bazy danych
            return RedirectToAction("Index"); // Przekierowanie po zapisaniu
        }
        catch (DbUpdateException ex)
        {
            // Obsługa błędów
            Console.WriteLine("Error: " + ex.InnerException?.Message);
            return View(order); // Powróć do widoku z błędem
        }
    }
}
