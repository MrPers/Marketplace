using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Models
{
    public class UserChoiceVM
    {
        public Guid StatusUserChoiceId { get; set; }
        public Guid ProductId { get; set; }
        public Guid PriceId { get; set; }
        public Guid UserChoiceId { get; set; }
        public Guid ShopId { get; set; }
        public int NumberProductinUserChoice { get; set; }
        public int NumberProductAll { get; set; }
        public string StatusUserChoiceName { get; set; }
        public string ProductName { get; set; }
        public string ProductPhoto { get; set; }
        public string ShopName { get; set; }
        public decimal NetPrice { get; set; }
    }
}
