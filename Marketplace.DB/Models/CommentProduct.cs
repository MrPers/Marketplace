using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.DB.Models
{
    public class CommentProduct : BaseEntity<long>
    {
        [Column(TypeName = "varchar(20)")]
        [Required]
        public string Text { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public DateTime DepartureDate { get; set; }
    }
}
