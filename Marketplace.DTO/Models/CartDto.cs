using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.DTO.Models
{
    public class CartDto : BaseEntityDto<Guid>
    {
        public int NumberProduct { get; set; }        
        public Guid StatusCartId { get; set; }
        public StatusCartDto StatusCart { get; set; }
        public Guid ProductId { get; set; }
        public FullProductDto Product { get; set; }
        public Guid UserId { get; set; }
        public UserDto User { get; set; }
        public string StatusCartName { get; set; }
        public string ProductName { get; set; }
        public string UserName { get; set; }
    }
}
