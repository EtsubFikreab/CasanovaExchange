namespace CasanovaExchange.Models
{
    public class Trade
    {
        public int TradeId { get; set; }
        public DateTime date { get; set; }
        public Commodity CommodityTraded { get; set; }
        public double PreviousClose { get; set; }
        public double Close { get; set; }
        public double Change { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Volume { get; set; }
    }
}
