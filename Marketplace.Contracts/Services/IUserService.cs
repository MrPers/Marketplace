using Marketplace.DTO.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Contracts.Services
{
    public interface IUserService
    {
        Task<ICollection<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(long id);
        Task AddAsync(UserDto user);
        Task UpdateAsync(long id, UserDto user);
        Task DeleteAsync(long id);
    }
}
