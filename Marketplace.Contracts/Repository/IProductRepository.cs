using Marketplace.DB.Models;
using Marketplace.DTO.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Contracts.Repository
{
    public interface IProductRepository : IBaseRepository<Product, ProductDto, Guid>
    {
        Task<ICollection<ProductDto>> GetProductByShopId(Guid id);
    }
}
