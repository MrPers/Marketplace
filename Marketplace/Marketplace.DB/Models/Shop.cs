using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.DB.Models
{
    public class Shop : BaseEntity<Guid>
    {
        [Column(TypeName = "varchar(20)")]
        [Required]
        public string Name { get; set; }
        public virtual ICollection<UserRoleShop> UserRoles { get; set; } = new List<UserRoleShop>();
        public virtual ICollection<Price> Prices { get; set; } = new List<Price>();
    }
}
