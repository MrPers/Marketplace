using Marketplace.DTO.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.DB.Models
{
    //public class Claim : System.Security.Claims.Claim, IBaseEntity<Guid>
    //public class Claim : BaseEntity<Guid>
    public class Claim : BaseEntity<Guid>
    {
        [Column(TypeName = "varchar(30)")]
        public string Name { get; set; }
        public ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}
