using System.ComponentModel.DataAnnotations.Schema;

namespace CasanovaExchange.Models
{
    public class CommodityListing
    {
        //Commodities up for sale
        public int Id { get; set; }
        public Commodity Commodity { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime DateListed { get; set; }
        public bool active { get; set; }
    }
}
