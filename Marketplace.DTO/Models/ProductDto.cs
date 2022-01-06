using System;
using System.Collections.Generic;

namespace Marketplace.DTO.Models
{
    public class ProductDto : BaseEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Photo { get; set; }
        public ICollection<PriceDto> Prices { get; set; } = new List<PriceDto>();
        public ICollection<CartDto> Carts { get; set; } = new List<CartDto>();
        public ICollection<CommentProductDto> CommentProducts { get; set; } = new List<CommentProductDto>();
    }
}