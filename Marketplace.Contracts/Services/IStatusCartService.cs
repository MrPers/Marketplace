using Marketplace.DTO.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Contracts.Services
{
    public interface ICartService
    {
        Task<ICollection<CartDto>> GetAllAsync();
        Task<CartDto> GetByIdAsync(Guid id);
        Task AddAsync(CartDto claim);
        Task UpdateAsync(CartDto claim);
        Task DeleteAsync(Guid id);
    }
}
