using CasanovaExchange.Models;

namespace CasanovaExchange.Repository
{
    public interface ICommodityRepository
    {
        public bool buyCommodity(int id, double PurchaseQuantity, string userId);
        public List<Warehouse> GetWarehouseList();
        public List<string> GetCommodityList();

	}
}
