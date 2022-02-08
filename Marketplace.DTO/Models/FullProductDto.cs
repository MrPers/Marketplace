using System;
using System.Collections.Generic; 

namespace Marketplace.DTO.Models
{
    public class FullProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public string ProductGroupName { get; set; }
        public decimal NetPrice { get; set; }
        public bool PricesAverage { get; set; } = false;
        public Guid ProductGroupId { get; set; }
    }
}