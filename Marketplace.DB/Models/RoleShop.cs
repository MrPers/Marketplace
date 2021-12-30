using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.DB.Models
{
     public class RoleShop : BaseEntity<long>
    {
        public long UserRoleId { get; set; }
        public UserRole UserRole { get; set; }
        public long UserShopId { get; set; }
        public UserShop UserShop { get; set; }
    }
}
