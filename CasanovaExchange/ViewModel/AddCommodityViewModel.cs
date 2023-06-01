using CasanovaExchange.Models;

namespace CasanovaExchange.ViewModel
{
    public class AddCommodityViewModel
    {
        public Commodity? Commodity { get; set; }
        public List<Warehouse>? Warehouse { get; set; }
    }
}
