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
            CurrentUserPortfolio = GetPortfolio(userId);
            commodityListingViewModel.commodity = GetCommodityById(commodityListingViewModel.commodity.Id);
            //Add quantity to personal Commodity list
            if (_context.UserCommodity.Where(uc => uc.Commodity == commodityListingViewModel.commodity).FirstOrDefault() == null)
            {
                _context.UserCommodity.Add(new()
                {
                    Portfolio = GetPortfolio(userId),
                    Commodity = commodityListingViewModel.commodity,
                    Quantity = commodityListingViewModel.Quantity
                });
            }
            else
            {
                _context.UserCommodity.Where(uc => uc.Commodity == commodityListingViewModel.commodity && uc.Portfolio == GetPortfolio(userId)).FirstOrDefault().Quantity += commodityListingViewModel.Quantity;
            }

            //Buy from listing
            commodityListingViewModel.CommodityListings = GetCommodityListings(commodityListingViewModel.commodity);
            double price = 0, quantityLeft = commodityListingViewModel.Quantity;
            for (int i = 0; i < commodityListingViewModel.CommodityListings.Count() && quantityLeft > 0; i++)
            {
                if (quantityLeft <= commodityListingViewModel.CommodityListings[i].Quantity)
                {
                    price +=  quantityLeft * commodityListingViewModel.CommodityListings[i].Price;
                    //buyer
                    _context.CommodityTransactions.Add(new()
                    {
                        Commodity = commodityListingViewModel.commodity,
                        Price = -commodityListingViewModel.CommodityListings[i].Price,
                        Quantity = quantityLeft,
                        Portfolio = GetPortfolio(userId)
                    });
                    //seller
                    _context.CommodityTransactions.Add(new()
                    {
                        Commodity = commodityListingViewModel.commodity,
                        Price = commodityListingViewModel.CommodityListings[i].Price,
                        Quantity = -quantityLeft,
                        Portfolio = commodityListingViewModel.CommodityListings[i].Portfolio
                    });
                    CurrentUserPortfolio = _context.Portfolio.Where(Portfolio => Portfolio == commodityListingViewModel.CommodityListings[i].Portfolio).FirstOrDefault();
                    //_context.Portfolio.Where(p => p == CurrentUserPortfolio).Include(p => p.Wallet).FirstOrDefault().Wallet.Balance += price;
                    _context.UserCommodity.Where(uc => uc.Commodity == commodityListingViewModel.commodity && uc.Portfolio == CurrentUserPortfolio).FirstOrDefault().Quantity -= quantityLeft;
                    _context.CommodityListing.Find(commodityListingViewModel.CommodityListings[i].Id).Quantity -= quantityLeft;
                    quantityLeft = 0;
                }
                else
                {
                    price += quantityLeft * commodityListingViewModel.CommodityListings[i].Price;
                    //buyer
                    _context.CommodityTransactions.Add(new()
                    {
                        Commodity = commodityListingViewModel.commodity,
                        Price = -commodityListingViewModel.CommodityListings[i].Price,
                        Quantity = commodityListingViewModel.CommodityListings[i].Quantity,
                        Portfolio = GetPortfolio(userId)
                    });
                    //seler
                    _context.CommodityTransactions.Add(new()
                    {
                        Commodity = commodityListingViewModel.commodity,
                        Price = commodityListingViewModel.CommodityListings[i].Price,
                        Quantity = -commodityListingViewModel.CommodityListings[i].Quantity,
                        Portfolio = commodityListingViewModel.CommodityListings[i].Portfolio
                    });
                    CurrentUserPortfolio = _context.Portfolio.Where(Portfolio => Portfolio == commodityListingViewModel.CommodityListings[i].Portfolio).FirstOrDefault();
                    //_context.Portfolio.Where(p => p == CurrentUserPortfolio).Include(p => p.Wallet).FirstOrDefault().Wallet.Balance += price;
                    _context.UserCommodity.Where(uc => uc.Commodity == commodityListingViewModel.commodity && uc.Portfolio == CurrentUserPortfolio).FirstOrDefault().Quantity = 0;

                    quantityLeft -= commodityListingViewModel.CommodityListings[i].Quantity;
                    _context.CommodityListing.Find(commodityListingViewModel.CommodityListings[i].Id).Quantity = 0;
                }
            }
            _context.Portfolio.Where(p => p.UserId == userId).Include(p => p.Wallet).FirstOrDefault().Wallet.Balance -= price;
            _context.SaveChanges();
            return true;
        }
        public void SellCommodity(CommodityListingViewModel commodityListingViewModel, string currentUserId)
        {
            Commodity commodity = GetCommodityById(commodityListingViewModel.commodity.Id);
            CommodityListing commodityListing = new CommodityListing() 
            {
                Commodity = commodity,
                Quantity = commodityListingViewModel.CommodityListing.Quantity,
                Price = commodityListingViewModel.CommodityListing.Price,
                DateListed = DateTime.Now,
                Active = false,
                Portfolio=GetPortfolio(currentUserId)
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
            return _context.Warehouse.Where(w => w.WarehouseCode == WarehouseCode).FirstOrDefault();
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
            return _context.CommodityListing.Where(cl => cl.Commodity == commodity && cl.Quantity > 0).OrderBy(cl => cl.Price).ToList();
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
