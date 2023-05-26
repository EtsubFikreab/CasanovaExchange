using Microsoft.AspNetCore.Identity;

namespace CasanovaExchange.Models
{
    public class Portfolio
    {
        public int PortfolioId { get; set;}
        public IdentityUser User { get; set; }
        public List <CommodityTransaction> CommodityTransaction { get; set; }
        public List <CommodityListing> CommodityListings { get; set; }
        public Wallet Wallet { get; set; }
    }
}
