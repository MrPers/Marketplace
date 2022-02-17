using System;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Models
{
    public class CommentProductVM
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(4)]
        public string Text { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public DateTime DepartureDate { get; set; }
        public string UserName { get; set; }
    }
}
