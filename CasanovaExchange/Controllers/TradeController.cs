using CasanovaExchange.Models;
using CasanovaExchange.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

		// GET: Trade/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Trade/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
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

		// GET: Trade/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: Trade/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
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

		// GET: Trade/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: Trade/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
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
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult BuyProduct(int id, IFormCollection collection)
		{
			try
			{
				_userManager.GetUserId(User);

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
			return View();
		}
	}
}
