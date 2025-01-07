using Microsoft.EntityFrameworkCore;
using zaliczenie.Models;

namespace zaliczenie.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ustawienie klucza głównego dla encji User
            modelBuilder.Entity<User>()
                .HasKey(u => u.Email);
        }
    }
}
