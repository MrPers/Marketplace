using System.Collections.Generic;

namespace Marketplace.DB.Models
{
    public class UserShop: BaseEntity<long>
    {
        public long ShopId { get; set; }
        public long UserId { get; set; }
        public Shop Shop { get; set; }
        public User User { get; set; }
        public ICollection<RoleShop> RoleShops { get; set; } = new List<RoleShop>();
    }
}
