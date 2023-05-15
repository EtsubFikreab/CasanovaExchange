namespace CasanovaExchange.Models
{
    public class Portfolio
    {
        public int PortfolioId { get; set;}
        public int UserId { get; set; }
        public Transaction transaction { get; set; }
    }
}
