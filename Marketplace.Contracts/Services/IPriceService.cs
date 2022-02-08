using Marketplace.DTO.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Contracts.Services
{
    public interface IPriceService
    {
        Task<ICollection<PriceDto>> GetAllAsync();
        Task<ICollection<PriceDto>> GetByProductIdAsync(Guid id);
        Task<PriceDto> GetByIdAsync(Guid id);
        Task AddAsync(PriceDto price);
        Task UpdateAsync(PriceDto price);
        Task DeleteAsync(Guid id);
    }
}
