using CasanovaExchange.Models;
using CasanovaExchange.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using System.Diagnostics;

namespace CasanovaExchange.Controllers
{

	public class HomeController : Controller
	{
		private readonly IHtmlLocalizer<HomeController> _localizer;
		private readonly ILogger<HomeController> _logger;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly ICommodityRepository _commodityRepository;
		public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, ICommodityRepository commodityRepository, IHtmlLocalizer<HomeController> localizer)
		{
			_userManager = userManager;
			_commodityRepository = commodityRepository;
			_logger = logger;
			_localizer = localizer;
		}
		[Authorize]
		public IActionResult Index()
		{
			var test = _localizer["HelloWorld"];
			ViewData["HelloWorld"] = test;
			_commodityRepository.CheckPortfolio(_userManager.GetUserId(User));
			return View();
		}
		[Authorize]
		public IActionResult Privacy()
		{
			return View();
		}
		[HttpPost]
		public IActionResult CultureManagement (string culture, string returnUrl)
        {
			Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
				new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) });
			return LocalRedirect(returnUrl);
				}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}