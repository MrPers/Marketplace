using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.DB.Models
{
    public class Product : BaseEntity<Guid>
    {
        [Column(TypeName = "varchar(20)")]
        [Required]
        public string Name { get; set; }
        [Required]
        public string Photo { get; set; }
        public string Description { get; set; }
        public ProductGroup ProductGroup { get; set; }
        public Guid ProductGroupId { get; set; }
        public ICollection<Price> Prices { get; set; } = new List<Price>();
        public ICollection<CommentProduct> CommentProducts { get; set; } = new List<CommentProduct>();
    }
}