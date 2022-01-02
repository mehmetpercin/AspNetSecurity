using Microsoft.EntityFrameworkCore;

namespace DataProtection.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Pen",
                    Price = 10,
                    Quantity = 100,
                },
                new Product
                {
                    Id = 2,
                    Name = "Paper A4",
                    Price = 1,
                    Quantity = 500,
                });

            //base.OnModelCreating(modelBuilder);
        }
    }
}
