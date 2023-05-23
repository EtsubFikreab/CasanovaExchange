namespace CasanovaExchange.Repository
{
    public interface ICommodityRepository
    {
        public bool buyCommodity(int id, double PurchaseQuantity, string userId);
    }
}
