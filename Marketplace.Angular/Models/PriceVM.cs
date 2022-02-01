using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Angular.Models
{
    public class PriceVM
    {
        public Guid Id { get; set; }
        public decimal NetPrice { get; set; }
        public int NumberProduct { get; set; }
        public Guid ProductId { get; set; }
        public Guid ShopId { get; set; }
    }
}
