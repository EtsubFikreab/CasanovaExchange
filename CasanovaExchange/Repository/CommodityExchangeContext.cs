using CasanovaExchange.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CasanovaExchange.Repository
{
    public class CommodityExchangeContext: IdentityDbContext
    {
        public CommodityExchangeContext(DbContextOptions<CommodityExchangeContext> options)
            : base(options)
        {

        }

        public DbSet<Commodity> commodity { get; set; }
        public DbSet<Trade> trade { get; set; }

    }
}
