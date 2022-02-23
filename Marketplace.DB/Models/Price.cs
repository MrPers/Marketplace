using System;
using System.Collections.Generic;

namespace Marketplace.DB.Models
{
    public class Price : BaseEntity<Guid>
    {
        public decimal NetPrice { get; set; }
        public int NumberProduct { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid ShopId { get; set; }
        public Shop Shop { get; set; }
        public ICollection<UserChoice> UserChoices { get; set; } = new List<UserChoice>();
    }
}
