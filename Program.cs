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

// Dodajemy sesjê
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Czas wygasania sesji
    options.Cookie.HttpOnly = true;                   // Zabezpieczenie przed dostêpem do cookie z JavaScriptu
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// U¿ycie sesji przed routingiem
app.UseSession();

// Inne middleware
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
