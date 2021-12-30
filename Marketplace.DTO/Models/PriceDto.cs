namespace Marketplace.DTO.Models
{
    public class PriceDto : BaseEntityDto<long>
    {
        public decimal NetPrice { get; set; }
        public int NumberProduct { get; set; }
        public long ProductId { get; set; }
        public ProductDto Product { get; set; }
        public long ShopId { get; set; }
        public ShopDto Shop { get; set; }
    }
}
