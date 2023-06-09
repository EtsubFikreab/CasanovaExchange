﻿using System.ComponentModel.DataAnnotations.Schema;

namespace CasanovaExchange.Models
{
    public class Commodity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Description { get; set; }
        public Warehouse CommodityWarehouse { get; set; }
        public int ProductionYear { get; set; }
        [NotMapped]
        public IFormFile? commodityImage { get; set; }
        public string? CommodityImagePath { get; set; }

    }
}
