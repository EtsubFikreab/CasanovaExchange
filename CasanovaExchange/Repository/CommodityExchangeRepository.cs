using CasanovaExchange.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Transactions;
using System.Linq;
using NuGet.Protocol;
using CasanovaExchange.ViewModel;

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
		public bool BuyCommodity(CommodityListingViewModel commodityListingViewModel, string userId)
		{
			if (_context.UserCommodity.Where(uc => uc.Commodity == commodityListingViewModel.commodity).FirstOrDefaultAsync() == null)
			{
				_context.UserCommodity.Add(new()
				{
					Portfolio = GetPortfolio(userId),
					Commodity = commodityListingViewModel.commodity,
					Quantity=commodityListingViewModel.Quantity
				});
			}
			else
			{
				//_context.UserCommodity.Where(uc => uc.Commodity == commodityListingViewModel.commodity).Include(uc=>uc.Quantity).FirstOrDefaultAsync().Quantity += commodityListingViewModel.Quantity;
			}


			_context.SaveChanges();
			return true;
		}
		public void SellCommodity(CommodityListingViewModel commodityListingViewModel)
		{
			Commodity commodity = GetCommodityById(commodityListingViewModel.commodity.Id);
			CommodityListing commodityListing = new()
			{
				Commodity = commodity,
				Quantity = commodityListingViewModel.CommodityListing.Quantity,
				Price = commodityListingViewModel.CommodityListing.Price,
				DateListed = DateTime.Now,
				Active = false
			};
			_context.CommodityListing.Add(commodityListing);
			_context.SaveChanges();
		}
		public List<Warehouse> GetWarehouseList()
		{
			return _context.Warehouse.ToList();
		}
		public Warehouse GetWarehouseByCode(string WarehouseCode)
		{
			return _context.Warehouse.Where(w=>w.WarehouseCode== WarehouseCode).FirstOrDefault();
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
		public Commodity GetCommodityById(int id)
		{
			return _context.Commodity.Where(x => x.Id == id).Include(w => w.CommodityWarehouse).FirstOrDefault();
		}
		public List<CommodityListing> GetCommodityListings(Commodity commodity)
		{
			return _context.CommodityListing.Where(cl => cl.Commodity == commodity).ToList();
		}





		public void CheckPortfolio(string CurrentUserId)
		{
			CurrentUserPortfolio = _context.Portfolio.Where(p => p.UserId == CurrentUserId).Include(p => p.Wallet).FirstOrDefault();
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
		public Portfolio GetPortfolio(string CurrentUserId)
		{
			CurrentUserPortfolio = _context.Portfolio.Where(p => p.UserId == CurrentUserId).Include(p => p.Wallet).FirstOrDefault();
			return CurrentUserPortfolio;
		}
		public void AddBalance(string CurrentUserId, double Balance)
		{
			_context.Portfolio.Where(p => p.UserId == CurrentUserId).Include(p => p.Wallet).FirstOrDefault().Wallet.Balance += Balance;
			_context.SaveChanges();
		}

		public bool AddWarehouse(Warehouse warehouse)
		{
			_context.Add(warehouse);
			_context.SaveChanges();
			return true;
		}

		public bool AddCommodity(Commodity commodity)

		{
            _context.Add(commodity);
			_context.SaveChanges();
			return true;
		}
	}
}
