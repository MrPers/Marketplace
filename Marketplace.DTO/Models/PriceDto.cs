using System;

namespace Marketplace.DTO.Models
{
    public class PriceDto : BaseEntityDto<Guid>
    {
        public decimal NetPrice { get; set; }
        public string ShopName { get; set; }
        public int NumberProduct { get; set; }
    }
}
