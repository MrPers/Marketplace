namespace Marketplace.DB.Models
{
    public class Price : BaseEntity<long>
    {
        public decimal NetPrice { get; set; }
        public int NumberProduct { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public long ShopId { get; set; }
        public Shop Shop { get; set; }
    }
}
