using Microsoft.EntityFrameworkCore;
using Pharmacy.WebApi.Models;

namespace Pharmacy.WebApi.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(email => email.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(phone => phone.PhoneNumber).IsUnique();
        }

        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Drug> Drugs { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;

    }
}
