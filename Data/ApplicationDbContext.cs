using Microsoft.EntityFrameworkCore;
using zaliczenie.Models;

namespace zaliczenie.Data
{
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {
            }

            public DbSet<User> Users { get; set; }
            public DbSet<Order> Orders { get; set; } // Tabela Orders
            public DbSet<OrderItem> OrderItems { get; set; } // Tabela OrderItems
            public DbSet<Product> Products { get; set; } // Tabela Products
        }
    }
