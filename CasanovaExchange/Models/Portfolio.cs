using Microsoft.AspNetCore.Identity;

namespace CasanovaExchange.Models
{
    public class Portfolio
    {
        public int PortfolioId { get; set;}
        public IdentityUser User { get; set; }
        public List <Transaction> transactions { get; set; }
        public Wallet Wallet { get; set; }
    }
}
