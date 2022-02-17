using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Marketplace.DTO.Models
{
    public class UserRoleDto : IdentityUserRole<Guid>, IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid ShopId { get; set; }
        public ShopDto Shop { get; set; }
    }
}
