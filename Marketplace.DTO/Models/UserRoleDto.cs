using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Marketplace.DTO.Models
{
    public class UserRoleDto : IdentityUserRole<long>, IBaseEntity<long>
    {
        public long Id { get; set; }
        public ICollection<RoleShopDto> RoleShops { get; set; }
    }
}
