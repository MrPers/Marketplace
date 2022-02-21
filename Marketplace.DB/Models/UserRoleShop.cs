using Marketplace.DTO.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.DB.Models
{
    public class UserRoleShop: IdentityUserRole<Guid>
    {
        //public Guid UserId { get; set; }
        //public Guid RoleId { get; set; }
        public Guid ShopId { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
        public Shop Shop { get; set; }
    }
}
