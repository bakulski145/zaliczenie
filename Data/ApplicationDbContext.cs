using Microsoft.EntityFrameworkCore;
using zaliczenie.Models;

namespace zaliczenie.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; } // Dodaj DbSet dla produktów
    }
}
