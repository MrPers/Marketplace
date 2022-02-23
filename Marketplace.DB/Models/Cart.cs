using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.DB.Models
{
    public class UserChoice : BaseEntity<Guid>
    {
        public int NumberProduct { get; set; }         
        public StatusUserChoice StatusUserChoice { get; set; }
        public Guid StatusUserChoiceId { get; set; }
        public Guid PriceId { get; set; }
        public Price Price { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
