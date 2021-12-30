using Marketplace.Contracts.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace Marketplace.DB.Models
{
    public class Role : IdentityRole<long>, IBaseEntity<long>
    {
        [Column(TypeName = "varchar(10)")]
        public override string Name { get; set; }
        public ICollection<Claim> Claims { get; set; } = new List<Claim>();
    }
}