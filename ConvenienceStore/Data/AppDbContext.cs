using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ConvenienceStore.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<StockTransaction> StockTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed initial data
            modelBuilder.Entity<InventoryItem>().HasData(
                new InventoryItem { Id = 1, Name = "Cooking Oil 1 litre", Description = "Item1", MaxStockQuantity = 50, CurrentStock = 0 },
                new InventoryItem { Id = 2, Name = "Coffee Mix", Description = "Item2", MaxStockQuantity = null, CurrentStock = 0 },
                new InventoryItem { Id = 3, Name = "Cooking Oil 5 litre", Description = "Item3", MaxStockQuantity = 20, CurrentStock = 0 },
                new InventoryItem { Id = 4, Name = "Rice 4 Kg bag", Description = "Item4", MaxStockQuantity = 50, CurrentStock = 0 },
                new InventoryItem { Id = 5, Name = "Instant Noodles", Description = "Item5", MaxStockQuantity = 50, CurrentStock = 0 }
            );
        }
    }
}