using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.DTO.Models
{
    public class ShopDto : BaseEntityDto<long>
    {
        public string Name { get; set; }
        public virtual ICollection<UserShopDto> UserShops { get; set; }
        public virtual ICollection<PriceDto> Prices { get; set; } 
    }
}
