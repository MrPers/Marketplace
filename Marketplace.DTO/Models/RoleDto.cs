using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Marketplace.DTO.Models
{
    public class RoleDto : IdentityRole<Guid>, IBaseEntity<Guid>
    {
        public override string Name { get; set; }
        public ICollection<ClaimDto> Claims { get; set; }
        public ICollection<string> ClaimNames { get; set; }
    }
}