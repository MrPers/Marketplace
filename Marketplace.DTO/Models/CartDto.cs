using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.DTO.Models
{
    public class CartDto : BaseEntityDto<long>
    {
        public int NumberProduct { get; set; }        
        public virtual ICollection<StatusCartDto> StatusCarts { get; set; }
        public long ProductId { get; set; }
        public ProductDto Product { get; set; }
        public long UserId { get; set; }
        public UserDto User { get; set; }
    }
}
