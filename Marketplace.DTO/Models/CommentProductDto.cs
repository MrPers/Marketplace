using System;

namespace Marketplace.DTO.Models
{
    public class CommentProductDto : BaseEntityDto<Guid>
    {
        public string Text { get; set; }
        public Guid ProductId { get; set; }
        public BriefProductDto Product { get; set; }
        public Guid UserId { get; set; }
        public UserDto User { get; set; }
        public DateTime DepartureDate { get; set; }
    }
}
