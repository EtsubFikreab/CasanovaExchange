using CasanovaExchange.Models;
using CasanovaExchange.Repository;
using CasanovaExchange.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CasanovaExchange.Controllers
{
    public class TradeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICommodityRepository _commodityRepository;
        public TradeController(UserManager<IdentityUser> userManager, ICommodityRepository commodityRepository)
        {
            _userManager = userManager;
            _commodityRepository = commodityRepository;
        }
        // GET: Trade
        public ActionResult Index()
        {
            return View();
        }

        // GET: Trade/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuyProduct(int id, IFormCollection collection)
        {
            try
            {

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult ListWarehouses()
        {
            List<Warehouse> warehouses = _commodityRepository.GetWarehouseList();
            return View(warehouses);
        }
        public ActionResult ListCommodities()
        {
            ViewBag.commodityLs = _commodityRepository.GetCommodityList();
            _commodityRepository.CheckPortfolio(_userManager.GetUserId(User));
            return View();
        }
        public ViewResult GetCommodity(string commodityString)
        {
            if (commodityString.IsNullOrEmpty())
                commodityString = "empty";
            ViewBag.Commodity = commodityString;
            List<Commodity> CommodityList = _commodityRepository.GetCommodityByName(commodityString);
            return View(CommodityList);
        }
        public ViewResult GetCommodityListings(int commodityId)
        {
            CommodityListingViewModel commodityListingViewModel = new()
            {
                commodity = _commodityRepository.GetCommodityById(commodityId)
            };
           
            commodityListingViewModel.CommodityListings = _commodityRepository.GetCommodityListings(commodityListingViewModel.commodity);
            commodityListingViewModel.CommodityListing = new CommodityListing()
            {
                Commodity = _commodityRepository.GetCommodityById(commodityId),
                Quantity = 0,
                Price = 0,
                Active = false
            };
            return View(commodityListingViewModel);
        }
        [HttpPost]
        public ViewResult BuyCommodity(CommodityListingViewModel commodityListingViewModel)
        {
            _commodityRepository.BuyCommodity(commodityListingViewModel, _userManager.GetUserId(User));
            return View(commodityListingViewModel);
        }
        [HttpPost]
        public ActionResult SellCommodity(CommodityListingViewModel commodityListingViewModel)
        {
            _commodityRepository.SellCommodity(commodityListingViewModel, _userManager.GetUserId(User));

            return RedirectToAction("Index", "Home");
        }
    }
}
