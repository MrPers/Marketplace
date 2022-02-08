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
        public string ShopName { get; set; }
        public int NumberProduct { get; set; }
    }
}
