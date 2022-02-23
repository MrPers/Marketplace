using Marketplace.DTO.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Contracts.Services
{
    public interface IStatusUserChoiceService
    {
        Task<ICollection<UserChoiceDto>> GetUsersChoiceByUserIdAsync(Guid Id);
        Task<ICollection<StatusUserChoiceDto>> GetAllAsync();
        Task<StatusUserChoiceDto> GetByIdAsync(Guid id);
        Task AddAsync(StatusUserChoiceDto statusUserChoice);
        Task UpdateAsync(StatusUserChoiceDto statusUserChoice);
        Task DeleteAsync(Guid id);
    }
}
