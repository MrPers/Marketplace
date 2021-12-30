using System;

namespace Marketplace.DTO.Models
{
    public class CommentProductDto : BaseEntityDto<long>
    {
        public string Text { get; set; }
        public long ProductId { get; set; }
        public ProductDto Product { get; set; }
        public long UserId { get; set; }
        public UserDto User { get; set; }
        public DateTime DepartureDate { get; set; }
    }
}
