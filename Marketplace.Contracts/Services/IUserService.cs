using Marketplace.DTO.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Contracts.Services
{
    public interface IUserService
    {
        Task<ICollection<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(Guid id);
        Task AddAsync(UserDto user);
        Task UpdateAsync(Guid id, UserDto user);
        Task DeleteAsync(Guid id);
    }
}
