using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using zaliczenie.Data;

var builder = WebApplication.CreateBuilder(args);

// Rejestracja DbContext z odpowiednim connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dodajemy sesję
builder.Services.AddDistributedMemoryCache(); // Dodajemy pamięć rozproszoną do sesji
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Czas wygasania sesji
    options.Cookie.HttpOnly = true;                   // Zabezpieczenie przed dostępem do cookie z JavaScriptu
    options.Cookie.IsEssential = true;                // Upewniamy się, że cookie jest niezbędne do działania aplikacji
});

// Rejestracja kontrolerów i widoków
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware sesji
app.UseSession();

// Middleware dla routingu i autoryzacji
app.UseRouting();
app.UseAuthorization(); // Dodajemy middleware dla autoryzacji

// Konfiguracja domyślnej trasy
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Uruchamiamy aplikację
app.Run();
