using Marketplace.Contracts.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.DB.Models
{
    public class UserShop: BaseEntity<long>
    {
        public long ShopId { get; set; }
        public Shop Shop { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public ICollection<RoleShop> RoleShops { get; set; } = new List<RoleShop>();
    }
}
