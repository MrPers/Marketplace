using Marketplace.DTO.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
 
namespace Marketplace.Contracts.Services
{
    public interface IProductService
    {
        Task<ICollection<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(Guid id);
        Task UpdateAsync(ProductDto product);
        Task DeleteAsync(Guid id);
        Task AddAsync(ProductDto fullProductDto);
    }
}
