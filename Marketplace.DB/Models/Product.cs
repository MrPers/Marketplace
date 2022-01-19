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
        public ICollection<Price> Prices { get; set; } = new List<Price>();
        public ProductGroup ProductGroup { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //public string ProductGroupName { get; set; }
        public ICollection<Cart> Carts { get; set; } = new List<Cart>();
        public ICollection<CommentProduct> CommentProducts { get; set; } = new List<CommentProduct>();
    }
}