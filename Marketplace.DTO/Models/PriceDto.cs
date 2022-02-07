using System;

namespace Marketplace.DTO.Models
{
    public class PriceDto : BaseEntityDto<Guid>
    {
        public decimal NetPrice { get; set; }
        public int NumberProduct { get; set; }
        public Guid ProductId { get; set; }
        public BriefProductDto Product { get; set; }
        public Guid ShopId { get; set; }
        public ShopDto Shop { get; set; }
        //public string ProductName { get; set; }
        //public string ShopName { get; set; }
    }
}
