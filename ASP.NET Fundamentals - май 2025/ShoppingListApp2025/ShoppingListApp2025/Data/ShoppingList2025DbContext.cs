using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingListApp2025.Models;

namespace ShoppingListApp2025.Data
{
    public class ShoppingList2025DbContext : IdentityDbContext
    {
        public ShoppingList2025DbContext(DbContextOptions<ShoppingList2025DbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>()
                .HasData
                (
                    new Product() { Id = 1, Name = "Cheese" },
                    new Product() { Id = 2, Name = "Milk" }

                );
            builder.Entity<ProductNote>()
                .HasData(
                    new ProductNote() { Id = 1, Content = "Buy Gouda", ProductId = 1 }, // За Cheese
                    new ProductNote() { Id = 2, Content = "2 Liters", ProductId = 2 }    // За Milk
                );


            base.OnModelCreating(builder);
        }


        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ProductNote> ProductNotes { get; set; } = null!;
    }
}
