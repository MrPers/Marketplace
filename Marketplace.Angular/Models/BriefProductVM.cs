using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Angular.Models
{
    public class  BriefProductVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public decimal NetPrice { get; set; }
        public bool PricesAverage { get; set; }
    }
}