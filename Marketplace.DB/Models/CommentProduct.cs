using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.DB.Models
{
    public class CommentProduct : BaseEntity<Guid>
    {
        [Column(TypeName = "varchar(20)")]
        [Required]
        public string Text { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime DepartureDate { get; set; }
    }
}
