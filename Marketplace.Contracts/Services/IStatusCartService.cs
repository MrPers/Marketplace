using Marketplace.DTO.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Contracts.Services
{
    public interface IUserChoiceService
    {
        Task<ICollection<UserChoiceDto>> GetUsersChoiceByUserIdAsync(Guid Id);
        Task<ICollection<UserChoiceDto>> GetAllAsync();
        Task<UserChoiceDto> GetByIdAsync(Guid id);
        Task AddAsync(UserChoiceDto claim);
        Task UpdateAsync(UserChoiceDto claim);
        Task DeleteAsync(Guid id);
    }
}
