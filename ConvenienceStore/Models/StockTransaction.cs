namespace ConvenienceStore.Models
{
    public class StockTransaction
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string TransactionType { get; set; } // "IN" or "OUT"
        public int Quantity { get; set; }
        public DateTime TransactionDate { get; set; }

        // Navigation property
        public InventoryItem Item { get; set; }
    }
}
