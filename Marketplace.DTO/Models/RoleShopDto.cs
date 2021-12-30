using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.DTO.Models
{
    public class RoleShopDto : BaseEntityDto<long>
    {
        public long UserRoleId { get; set; }
        public UserRoleDto UserRole { get; set; }
        public long UserShopId { get; set; }
        public UserShopDto UserShop { get; set; }
    }
}
