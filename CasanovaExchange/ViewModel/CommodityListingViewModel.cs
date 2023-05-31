using CasanovaExchange.Models;
namespace CasanovaExchange.ViewModel
{
	public class CommodityListingViewModel
	{
		public List<CommodityListing>? CommodityListings { get; set; }
		public CommodityListing? CommodityListing { get; set; }
		public double? Quantity { get; set; }
		public Commodity? commodity { get; set; }
	}
}
