using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.DB.Models
{
    //public class Claim : System.Security.Claims.Claim, IBaseEntity<long>
    public class Claim : BaseEntity<long>
    {
        [Column(TypeName = "varchar(10)")]
        public string Name { get; set; }
        public ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}
