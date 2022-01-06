using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.DTO.Models
{
    public class CartDto : BaseEntityDto<Guid>
    {
        public int NumberProduct { get; set; }        
        public virtual ICollection<StatusCartDto> StatusCarts { get; set; }
        public Guid ProductId { get; set; }
        public ProductDto Product { get; set; }
        public Guid UserId { get; set; }
        public UserDto User { get; set; }
    }
}
