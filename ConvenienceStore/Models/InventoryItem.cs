using System.ComponentModel.DataAnnotations;

namespace ConvenienceStore.Models
{
    public class InventoryItem
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public int? MaxStockQuantity { get; set; } // Null means no limit

        public int CurrentStock { get; set; }

        // Navigation property
        public ICollection<StockTransaction> StockTransactions { get; set; }
    }
}
