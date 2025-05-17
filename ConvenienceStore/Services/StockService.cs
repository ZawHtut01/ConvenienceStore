using ConvenienceStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ConvenienceStore.Services
{
    public class StockService
    {
        private readonly AppDbContext _context;

        public StockService(AppDbContext context)
        {
            _context = context;
        }

        // Get all inventory items
        public async Task<List<InventoryItem>> GetAllItemsAsync()
        {
            return await _context.InventoryItems.ToListAsync();
        }

        // Get item by ID
        public async Task<InventoryItem> GetItemByIdAsync(int id)
        {
            return await _context.InventoryItems.FindAsync(id);
        }

        // Stock In operation
        public async Task<StockResult> StockInAsync(int itemId, int quantity)
        {
            var item = await _context.InventoryItems.FindAsync(itemId);
            if (item == null)
                return new StockResult(false, "Item not found.");

            // Check if quantity exceeds max stock limit (if applicable)
            if (item.MaxStockQuantity.HasValue && (item.CurrentStock + quantity) > item.MaxStockQuantity)
            {
                return new StockResult(false, $"Cannot stock in. Maximum quantity for {item.Name} is {item.MaxStockQuantity}.");
            }

            // Update stock
            item.CurrentStock += quantity;

            // Record transaction
            var transaction = new StockTransaction
            {
                ItemId = itemId,
                TransactionType = "IN",
                Quantity = quantity,
                TransactionDate = DateTime.Now
            };

            _context.StockTransactions.Add(transaction);
            await _context.SaveChangesAsync();

            return new StockResult(true, $"Successfully stocked in {quantity} of {item.Name}.");
        }

        // Stock Out operation with business rules
        public async Task<StockResult> StockOutAsync(int itemId, int quantity)
        {
            var item = await _context.InventoryItems.FindAsync(itemId);
            if (item == null)
                return new StockResult(false, "Item not found.");

            // Check if enough stock is available
            if (item.CurrentStock < quantity)
                return new StockResult(false, $"Insufficient stock. Only {item.CurrentStock} available.");

            // Business Rule 4: Can't stock out Item4 if balance would be lower than 5
            if (item.Id == 4 && (item.CurrentStock - quantity) < 5)
                return new StockResult(false, "Cannot stock out Item4 if balance would be lower than 5.");

            // Business Rule 2: If Item1 is out of stock, don't allow stock out of Item3 if balance would be lower than 5
            if (item.Id == 3)
            {
                var item1 = await _context.InventoryItems.FindAsync(1);
                if (item1 != null && item1.CurrentStock == 0 && (item.CurrentStock - quantity) < 5)
                {
                    return new StockResult(false, "Cannot stock out Item3 when Item1 is out of stock and Item3 balance would be lower than 5.");
                }
            }

            // Update stock
            item.CurrentStock -= quantity;

            // Record transaction
            var transaction = new StockTransaction
            {
                ItemId = itemId,
                TransactionType = "OUT",
                Quantity = quantity,
                TransactionDate = DateTime.Now
            };

            _context.StockTransactions.Add(transaction);
            await _context.SaveChangesAsync();

            return new StockResult(true, $"Successfully stocked out {quantity} of {item.Name}.");
        }

        // Check if both Item1 and Item3 are being stocked out at the same time (for UI validation)
        public async Task<bool> AreItems1And3StockedOutTogetherAsync(List<int> itemIds)
        {
            var items = await _context.InventoryItems
                .Where(i => itemIds.Contains(i.Id) && (i.Id == 1 || i.Id == 3))
                .ToListAsync();

            return items.Count == 2; // Both Item1 and Item3 are in the list
        }

        // Helper class to return operation results
        public class StockResult
        {
            public bool Success { get; }
            public string Message { get; }

            public StockResult(bool success, string message)
            {
                Success = success;
                Message = message;
            }
        }
    }
}