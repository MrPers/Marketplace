using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.DB.Models
{
    public class Cart : BaseEntity<long>
    {
        public int NumberProduct { get; set; }        
        public virtual ICollection<StatusCart> StatusCarts { get; set; } = new List<StatusCart>();
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
    }
}
