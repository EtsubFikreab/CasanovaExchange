﻿using CasanovaExchange.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CasanovaExchange.Repository
{
	public class CommodityExchangeContext : IdentityDbContext
	{
		public CommodityExchangeContext(DbContextOptions<CommodityExchangeContext> options)
			: base(options)
		{

		}
		public DbSet<Trade> CurrentTrade { get; set; }
		public DbSet<CommodityTransaction> CommodityTransactions { get; set; }
		public DbSet<Portfolio> Portfolio { get; set; }
		public DbSet<Warehouse> Warehouse { get; set; }
		public DbSet<Commodity> Commodity { get; set; }
		public DbSet<CommodityListing> CommodityListing { get; set; }
		public DbSet<UserCommodity> UserCommodity { get; set; }
	}
}
