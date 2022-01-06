using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.DB.Models
{
    public class Cart : BaseEntity<Guid>
    {
        public int NumberProduct { get; set; }        
        public virtual ICollection<StatusCart> StatusCarts { get; set; } = new List<StatusCart>();
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
