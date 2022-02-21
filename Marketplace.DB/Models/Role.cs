using Marketplace.DTO.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.DB.Models
{
    public class Role : IdentityRole<Guid>, IBaseEntity<Guid>
    {
        public Role(string roleName) : base(roleName)
        {
        }
        public Role()
        {
        }

        [Column(TypeName = "varchar(30)")]
        public override string Name { get; set; }
        //public ICollection<Claim> Claims { get; set; } = new List<Claim>();
        public virtual ICollection<UserRoleShop> UserRoleShops { get; set; } = new List<UserRoleShop>();
    }
}