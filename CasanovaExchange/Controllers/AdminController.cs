using CasanovaExchange.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace CasanovaExchange.Controllers
{
	
    public class AdminController : Controller
    {
		private readonly ILogger<HomeController> _logger;

		public AdminController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Warehouse()
		{
			return View();
		}

		public IActionResult Commodity()
		{
			return View();
		}

	}
}
