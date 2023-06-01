using CasanovaExchange.Models;
using CasanovaExchange.Repository;
using CasanovaExchange.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace CasanovaExchange.Controllers
{

	public class AdminController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ICommodityRepository commodityRepository;
		private readonly IWebHostEnvironment webHostEnvironment;
		public AdminController(ILogger<HomeController> logger, ICommodityRepository commodityRepository, IWebHostEnvironment webHostEnvironment)
		{
			_logger = logger;
			this.commodityRepository = commodityRepository;
			this.webHostEnvironment = webHostEnvironment;
		}
		[HttpGet]
		public IActionResult Warehouse()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Warehouse(Warehouse warehouse)
		{
			if (!ModelState.IsValid)
			{
				return RedirectToAction("Admin", "Warehouse");
			}
			if (warehouse != null)
			{
				Warehouse w = new Warehouse
				{
					Name = warehouse.Name,
					WarehouseCode = warehouse.WarehouseCode,
				};
				if (commodityRepository.AddWarehouse(w))
				{
                    TempData["message"] = "Warhouse successfully added !!";
                    return RedirectToAction("Warehouse", "Admin");
				}
			}

			return View(warehouse);
		}

		[HttpGet]
		public IActionResult Commodity()
		{
			AddCommodityViewModel addCommodityViewModel = new()
			{

				//Commodity = commodity,
				Warehouse = commodityRepository.GetWarehouseList(),
			};
			
			// get warehouse list here
			// addCommodityViewModel.Warehouse =

            return View(addCommodityViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> AddCommodity(AddCommodityViewModel addCommodityViewModel)
        {
			if (addCommodityViewModel.Commodity.commodityImage != null)
			{
				addCommodityViewModel.Commodity.CommodityImagePath = UploadImage(addCommodityViewModel.Commodity.commodityImage);
			}
			Commodity userCommodity = new Commodity
			{
				Name = addCommodityViewModel.Commodity.Name,
				Symbol = addCommodityViewModel.Commodity.Symbol,
				Description = addCommodityViewModel.Commodity.Description,
				ProductionYear = addCommodityViewModel.Commodity.ProductionYear,
				CommodityImagePath=addCommodityViewModel.Commodity.CommodityImagePath,
				CommodityWarehouse = commodityRepository.GetWarehouseByCode( addCommodityViewModel.Commodity.CommodityWarehouse.WarehouseCode),
			};

			if (commodityRepository.AddCommodity(userCommodity))
			{
				TempData["message"] = "Commodity successfully added !!";
				return RedirectToAction("Commodity", "Admin");

			}

			return RedirectToAction("warehouse", "admin");

		}

		private string UploadImage(IFormFile file)
		{
			string imgPath = "Image/" + Guid.NewGuid().ToString() + "_" + file.FileName;
			string serverPath = Path.Combine(webHostEnvironment.WebRootPath, imgPath);
			 file.CopyTo(new FileStream(serverPath, FileMode.Create));
			return "/" + imgPath;
		}


	}
}
