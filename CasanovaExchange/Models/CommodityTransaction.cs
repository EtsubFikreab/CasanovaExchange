namespace CasanovaExchange.Models
{
    public class CommodityTransaction
    {
        public int Id { get; set; }
        public Commodity Commodity { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
        public Portfolio Portfolio { get; set; }
    }
}
