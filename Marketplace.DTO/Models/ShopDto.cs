using System.Collections.Generic;

namespace Marketplace.DTO.Models
{
    public class ShopDto : BaseEntityDto<long>
    {
        public string Name { get; set; }
        public virtual ICollection<UserShopDto> UserShops { get; set; }
        public virtual ICollection<PriceDto> Prices { get; set; }
    }
}
