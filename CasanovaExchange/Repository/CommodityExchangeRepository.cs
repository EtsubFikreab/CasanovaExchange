using CasanovaExchange.Models;
using Microsoft.AspNetCore.Identity;
using System.Transactions;

namespace CasanovaExchange.Repository
{
	public class CommodityExchangeRepository : ICommodityRepository
	{
		private readonly CommodityExchangeContext _context;
		private readonly HttpContext _httpContext;
		public CommodityExchangeRepository(CommodityExchangeContext context)
		{
			_context = context;
		}
		public bool buyCommodity(int id, double PurchaseQuantity, string userId)
		{
			Trade currentTrade = _context.CurrentTrade.Where(x => x.CommodityTraded.Id == id).FirstOrDefault();

			if (currentTrade.Volume < 0 || currentTrade.Volume < PurchaseQuantity)
				return false;

			CommodityTransaction commodityTransaction = new CommodityTransaction
			{
				Commodity = currentTrade.CommodityTraded,
				Price = currentTrade.Low,
				Quantity = PurchaseQuantity,
				Portfolio = _context.Portfolio.Where(x => x.User.Id == userId).FirstOrDefault()
			};
			_context.CommodityTransactions.Add(commodityTransaction);
			return true;
		}
		public List<Warehouse> GetWarehouseList()
		{
			return _context.Warehouse.ToList();
		}
		public List<string> GetCommodityList()
		{
			List<string> CommodityList = new List<string>();
			List<Commodity> commodities = _context.Commodity.ToList();
			foreach (var c in commodities)
			{
				if (!CommodityList.Contains(c.Name))
					CommodityList.Add(c.Name);
			}
			return CommodityList;
		}
	}
}
