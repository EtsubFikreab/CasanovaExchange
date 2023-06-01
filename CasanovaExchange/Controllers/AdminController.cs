using CasanovaExchange.Models;
using CasanovaExchange.Repository;
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
		public AdminController(ILogger<HomeController> logger, ICommodityRepository commodityRepository,IWebHostEnvironment webHostEnvironment)
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
                    return RedirectToAction("commodity", "Admin");
            }

            return View(warehouse);
        }

        [HttpGet]
        public IActionResult Commodity()
		{
			return View();
		}
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Commodity(Commodity commodity )
        {
           

            if (commodity.commodityImage != null)
            {
                commodity.CommodityImagePath = UploadImage(commodity.commodityImage);
            }
            Commodity userCommodity = new Commodity
            {
                Name = commodity.Name,
                   Symbol = commodity.Symbol,
                Description = commodity.Description,
                ProductionYear = commodity.ProductionYear,
                
                  CommodityWarehouse = commodity.CommodityWarehouse,

            };

            if (commodityRepository.AddCommodity(userCommodity))
                return RedirectToAction("Commodity", "Admin");

            return RedirectToAction("warehouse", "admin");

        }

        private string UploadImage(IFormFile file)
        {
            string imgPath = "Product/Image/" + Guid.NewGuid().ToString() + "_" + file.FileName;
            string serverPath = Path.Combine(webHostEnvironment.WebRootPath, imgPath);
            // file.CopyTo(new FileStream(serverPath, FileMode.Create));
            return "/" + imgPath;
        }


    }
}
