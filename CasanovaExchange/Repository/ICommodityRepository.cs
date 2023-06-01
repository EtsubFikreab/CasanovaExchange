using CasanovaExchange.Models;
using CasanovaExchange.ViewModel;

namespace CasanovaExchange.Repository
{
    public interface ICommodityRepository
    {
        public bool BuyCommodity(CommodityListingViewModel commodityListingViewModel, string userId);
		public List<Warehouse> GetWarehouseList();
        public List<string> GetCommodityList();
        public List<Commodity> GetCommodityByName(string commodityName);
        public void CheckPortfolio(string UserId);
        public Wallet GetWallet(string CurrentUserId);
        public void AddBalance(string CurrentUserId, double Balance);
        public Portfolio GetPortfolio(string CurrentUserId);
        public Commodity GetCommodityById(int id);
        public List<CommodityListing> GetCommodityListings(Commodity commodity);
        public void SellCommodity(CommodityListingViewModel commodityListingViewModel);
        public bool AddWarehouse(Warehouse warehouse);
        public bool AddCommodity(Commodity commodity);
	}
}
