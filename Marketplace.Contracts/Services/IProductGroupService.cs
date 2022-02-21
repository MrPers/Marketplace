using Marketplace.DTO.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Contracts.Services
{
    public interface IProductGroupService
    {
        Task<ICollection<ProductGroupDto>> GetAllAsync();
        Task<ProductGroupDto> GetByIdAsync(Guid id);
        Task AddAsync(ProductGroupDto product);
        Task UpdateAsync(ProductGroupDto product);
        Task DeleteAsync(Guid id);
    }
}
