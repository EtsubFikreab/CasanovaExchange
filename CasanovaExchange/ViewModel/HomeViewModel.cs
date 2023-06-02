using CasanovaExchange.Models;

namespace CasanovaExchange.ViewModel
{
	public class HomeViewModel
	{
		public List<CommodityListing> CommodityListing { get; set; }
		public List<UserCommodity> UserCommodities { get; set; }
		public List<CommodityTransaction> CommodityTransactions { get; set;}
		public List<Commodity> Commodities { get; set; }
	}
}
