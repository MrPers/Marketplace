using Marketplace.DTO.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Contracts.Services
{
    public interface IStatusCartService
    {
        Task<ICollection<StatusCartDto>> GetAllAsync();
        Task<StatusCartDto> GetByIdAsync(Guid id);
        Task AddAsync(StatusCartDto statusCart);
        Task UpdateAsync(StatusCartDto statusCart);
        Task DeleteAsync(Guid id);
    }
}
