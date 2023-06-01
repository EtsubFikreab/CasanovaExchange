namespace CasanovaExchange.Models
{
	public class UserCommodity
	{
		public int Id { get; set; }
		public Portfolio Portfolio { get; set; }
		public Commodity Commodity { get; set; }
		public double Quantity { get; set; }
	}
}
