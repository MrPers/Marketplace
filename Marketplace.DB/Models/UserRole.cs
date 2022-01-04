using Marketplace.DTO.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.DB.Models
{
    public class UserRole : IdentityUserRole<long>, IBaseEntity<long>
    {
        [Key]
        public long Id { get; set; }
        public ICollection<RoleShop> RoleShops { get; set; } = new List<RoleShop>();
    }
}
