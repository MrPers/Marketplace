using Marketplace.DB.Models;
using Marketplace.DTO.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Contracts.Services
{
    public interface IShopService
    {
        Task<ICollection<ShopDto>> GetAllAsync();
        Task<ShopDto> GetByIdAsync(Guid id);
        Task AddAsync(ShopDto shop);
        Task UpdateAsync(ShopDto shop);
        Task DeleteAsync(Guid id);
    }
}
