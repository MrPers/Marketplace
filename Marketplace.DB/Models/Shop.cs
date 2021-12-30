using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.DB.Models
{
    public class Shop : BaseEntity<long>
    {
        [Column(TypeName = "varchar(20)")]
        [Required]
        public string Name { get; set; }
        public virtual ICollection<UserShop> UserShops { get; set; } = new List<UserShop>();
        public virtual ICollection<Price> Prices { get; set; } = new List<Price>();
    }
}
