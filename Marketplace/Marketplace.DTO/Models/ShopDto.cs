using System;
using System.Collections.Generic;

namespace Marketplace.DTO.Models
{
    public class ShopDto : BaseEntityDto<Guid>
    {
        public string Name { get; set; }
        public virtual ICollection<UserRoleDto> UserRoles { get; set; }
        public virtual ICollection<PriceDto> Prices { get; set; }
    }
}
