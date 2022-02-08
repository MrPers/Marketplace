using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Angular.Models
{
    public class FullProductVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public string ProductGroupName { get; set; }
        public Guid ProductGroupId { get; set; }
    }
}