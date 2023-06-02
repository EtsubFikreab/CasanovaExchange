using CasanovaExchange.Models;
using CasanovaExchange.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CasanovaExchange.Controllers
{

	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly ICommodityRepository _commodityRepository;
		public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, ICommodityRepository commodityRepository)
		{
			_userManager = userManager;
			_commodityRepository = commodityRepository;
			_logger = logger;
		}


		[Authorize]
		public IActionResult Index()
		{
			return View();
		}


		[Authorize]
		public IActionResult Privacy()
		{
			return View();
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}