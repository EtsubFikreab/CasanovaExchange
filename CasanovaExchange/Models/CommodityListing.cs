namespace CasanovaExchange.Models
{
    public class CommodityListing
    {
        //Commodities up for sale
        public int Id { get; set; }
        public Commodity Commodity { get; set; }
        public double Quantity { get; set; }
        public DateTime DateListed { get; set; }
    }
}
