using System;
using System.Linq;

namespace ConvenienceStore.Models
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            // Ensure the database is created
            context.Database.EnsureCreated();

            // Check if we already have data
            if (context.InventoryItems.Any())
            {
                return; // DB has been seeded
            }

            // Seed initial inventory items
            var inventoryItems = new InventoryItem[]
            {
                new InventoryItem { Name = "Cooking Oil 1 litre", Description = "Item1", MaxStockQuantity = 50, CurrentStock = 0 },
                new InventoryItem { Name = "Coffee Mix", Description = "Item2", MaxStockQuantity = null, CurrentStock = 0 },
                new InventoryItem { Name = "Cooking Oil 5 litre", Description = "Item3", MaxStockQuantity = 20, CurrentStock = 0 },
                new InventoryItem { Name = "Rice 4 Kg bag", Description = "Item4", MaxStockQuantity = 50, CurrentStock = 0 },
                new InventoryItem { Name = "Instant Noodles", Description = "Item5", MaxStockQuantity = 50, CurrentStock = 0 }
            };

            context.InventoryItems.AddRange(inventoryItems);
            context.SaveChanges();
        }
    }
}