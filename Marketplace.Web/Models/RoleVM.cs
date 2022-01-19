using Marketplace.DTO.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Marketplace.Web.Models
{
    public class RoleVM : IdentityRole<Guid>, IBaseEntity<Guid>
    {
        public override string Name { get; set; }
        public ICollection<ClaimDto> Claims { get; set; }
    }
}