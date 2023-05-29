using Microsoft.AspNetCore.Identity;

namespace CasanovaExchange.Models
{
    public class Portfolio
    {
        public int Id { get; set;}
        public string UserId { get; set; }
        public List <CommodityListing> CommodityListings { get; set; }
        public Wallet Wallet { get; set; }
    }
}
