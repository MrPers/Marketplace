using Marketplace.DTO.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.DB.Models
{
    public class Role : IdentityRole<long>, IBaseEntity<long>
    {
        [Column(TypeName = "varchar(30)")]
        public override string Name { get; set; }
        public ICollection<Claim> Claims { get; set; } = new List<Claim>();
    }
}