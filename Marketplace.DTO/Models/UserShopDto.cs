using Marketplace.Contracts.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.DTO.Models
{
    public class UserShopDto
    {
        public long Id { get; set; }
        public long ShopId { get; set; }
        public ShopDto Shop { get; set; }
        public long UserId { get; set; }
        public UserDto User { get; set; }
        public ICollection<RoleShopDto> RoleShops { get; set; }
    }
}
