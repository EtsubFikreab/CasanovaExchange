namespace CasanovaExchange.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public Commodity commodity { get; set; }
        public decimal PriceBought { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal QuantityBought { get; set; }

    }
}
