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
        Task UpdatePasswordAsync(Guid id, string oldPassword, string newPassword);
        Task DeleteAsync(Guid id);
    }
}
