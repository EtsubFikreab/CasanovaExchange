using CasanovaExchange.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Transactions;
using System.Linq;
using NuGet.Protocol;

namespace CasanovaExchange.Repository
{
	public class CommodityExchangeRepository : ICommodityRepository
	{
		private readonly CommodityExchangeContext _context;
		private Portfolio CurrentUserPortfolio;
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
				//Portfolio = _context.Portfolio.Where(x => x.PortfolioUser.Id == userId).FirstOrDefault()
			};
			//_context.CommodityTransactions.Add(commodityTransaction);
			_context.SaveChanges();
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
		public List<Commodity> GetCommodityByName(string commodityName)
		{
			List<Commodity> CommodityList = new List<Commodity>();
			CommodityList = _context.Commodity.Where(x => x.Name == commodityName).Include(w => w.CommodityWarehouse).ToList();
			return CommodityList;
		}
		public void CheckPortfolio(string CurrentUserId)
		{
			CurrentUserPortfolio = _context.Portfolio.Where(p => p.UserId == CurrentUserId).Include(p=>p.Wallet).FirstOrDefault();
			if (CurrentUserPortfolio != null)
				return;

			Portfolio portfolio = new Portfolio
			{
				UserId = CurrentUserId,
				Wallet = new Wallet()
			};
			_context.Portfolio.Add(portfolio);
			_context.SaveChanges();
			CurrentUserPortfolio = _context.Portfolio.Where(p => p.UserId == CurrentUserId).Include(p => p.Wallet).FirstOrDefault();
		}
		public Wallet GetWallet(string CurrentUserId)
		{
			CurrentUserPortfolio = _context.Portfolio.Where(p => p.UserId == CurrentUserId).Include(p => p.Wallet).FirstOrDefault();
			return CurrentUserPortfolio.Wallet;
		}
		public Portfolio GetPortfolio()
		{
			return CurrentUserPortfolio;
		}
		public void AddBalance(string CurrentUserId, double Balance)
		{
			_context.Portfolio.Where(p => p.UserId == CurrentUserId).Include(p => p.Wallet).FirstOrDefault().Wallet.Balance+=Balance;
			_context.SaveChanges();

		}
	}
}
