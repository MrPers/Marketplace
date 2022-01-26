using System;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Web.Models
{
    public class ProductGroupVM
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(4)]
        public string Name { get; set; }
    }
}
