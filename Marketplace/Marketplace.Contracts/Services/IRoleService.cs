using Marketplace.DTO.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Contracts.Services
{
    public interface IRoleService
    {
        Task<ICollection<RoleDto>> GetAllAsync();
        Task<RoleDto> GetByIdAsync(Guid id);
        Task AddAsync(RoleDto role);
        Task UpdateAsync(RoleDto role);
        Task DeleteAsync(Guid id);
    }
}
