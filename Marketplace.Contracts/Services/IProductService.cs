using Marketplace.DTO.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Contracts.Services
{
    public interface IProductService
    {
        Task<ICollection<BriefProductDto>> GetAllAsync();
        Task<BriefProductDto> GetByIdAsync(Guid id);
        Task UpdateAsync(FullProductDto product);
        Task DeleteAsync(Guid id);
        Task AddAsync(FullProductDto fullProductDto);
    }
}
