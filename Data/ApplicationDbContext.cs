using Microsoft.EntityFrameworkCore;
using zaliczenie.Models;

namespace zaliczenie.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


            public DbSet<Product> Products { get; set; }
            public DbSet<User> Users { get; set; }
            public DbSet<Order> Orders { get; set; }

    }
}
