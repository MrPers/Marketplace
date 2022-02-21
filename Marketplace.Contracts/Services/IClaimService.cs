using Marketplace.DTO.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Contracts.Services
{
    public interface IClaimService
    {
        Task<ICollection<ClaimDto>> GetAllAsync();
        Task<ClaimDto> GetByIdAsync(int id);
        Task AddAsync(ClaimDto claim);
        Task UpdateAsync(ClaimDto claim);
        Task DeleteAsync(int id);
    }
}
