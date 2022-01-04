using System.Collections.Generic;

namespace Marketplace.DTO.Models
{
    public class UserShopDto : BaseEntityDto<long>
    {
        public long ShopId { get; set; }
        public ShopDto Shop { get; set; }
        public long UserId { get; set; }
        public UserDto User { get; set; }
        public ICollection<RoleShopDto> RoleShops { get; set; }
    }
}
