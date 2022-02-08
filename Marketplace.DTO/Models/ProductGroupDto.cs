using System;
using System.Collections.Generic;

namespace Marketplace.DTO.Models
{
    public class ProductGroupDto : BaseEntityDto<Guid>
    {
        public string Name { get; set; }
        public ICollection<FullProductDto> Products { get; set; }
    }
}
