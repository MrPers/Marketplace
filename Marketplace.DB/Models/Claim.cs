using Marketplace.DTO.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace Marketplace.DB.Models
{
    //public class Claim : System.Security.Claims.Claim, IBaseEntity<Guid>
    public class Claim : IdentityRoleClaim<Guid>, IBaseEntity<int>
    //public class Claim : BaseEntity<Guid>
    {
        //public Claim(string type, string value)
        //{

        //}

        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public override int Id { get; set; }
        //[Column(TypeName = "varchar(30)")]
        //public string Name { get; set; }
        //public ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}
