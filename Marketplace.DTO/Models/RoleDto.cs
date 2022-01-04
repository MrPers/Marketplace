using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Marketplace.DTO.Models
{
    public class RoleDto : IdentityRole<long>, IBaseEntity<long>
    {
        public override string Name { get; set; }
        public ICollection<ClaimDto> Claims { get; set; }
    }
}