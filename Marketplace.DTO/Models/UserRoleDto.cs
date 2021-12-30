using System.Collections.Generic;

namespace Marketplace.DTO.Models
{
    public class UserRoleDto : BaseEntityDto<long>
    {
        public ICollection<RoleShopDto> RoleShops { get; set; }
    }
}
