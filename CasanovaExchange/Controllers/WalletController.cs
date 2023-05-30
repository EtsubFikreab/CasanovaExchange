using CasanovaExchange.Models;
using CasanovaExchange.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CasanovaExchange.Controllers
{
	public class WalletController : Controller
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly ICommodityRepository _commodityRepository;
		public WalletController(UserManager<IdentityUser> userManager, ICommodityRepository commodityRepository)
		{
			_userManager = userManager;
			_commodityRepository = commodityRepository;
		}
		public IActionResult Index()
		{
			Wallet wallet = _commodityRepository.GetWallet(_userManager.GetUserId(User));
			return View(wallet);
		}
		[HttpPost]
		public IActionResult AddMoney(Wallet addBalance)
		{
			if (addBalance.Balance > 0)
			{
				_commodityRepository.AddBalance(_userManager.GetUserId(User), addBalance.Balance);
			}
			return RedirectToAction("Index");
		}
	}
}
