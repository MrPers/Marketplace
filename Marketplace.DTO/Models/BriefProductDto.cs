using System;
using System.Collections.Generic; 

namespace Marketplace.DTO.Models
{
    public class BriefProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public decimal NetPrice { get; set; }
    }
}