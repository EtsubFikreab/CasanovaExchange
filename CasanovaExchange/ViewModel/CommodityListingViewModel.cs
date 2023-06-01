using CasanovaExchange.Models;
namespace CasanovaExchange.ViewModel
{
	public class CommodityListingViewModel
	{
		public List<CommodityListing>? CommodityListings { get; set; }
		public CommodityListing? CommodityListing { get; set; }
		public double Quantity { get; set; }//for buying commodity
		public Commodity commodity { get; set; }
	}
}
