using Marketplace.DTO.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Contracts.Repository
{
    public interface IBaseRepository<TTable, TDto, TId> where TTable : IBaseEntity<TId>
    {
        Task<ICollection<TDto>> GetAllAsync();
        Task<TDto> GetByIdAsync(TId Id);
        Task<TId> AddAsync(IEnumerable<TDto> Dto);
        Task<TId> AddAsync(TDto Dto);
        Task UpdateAsync(TId Id, TDto meaning); 
        Task DeleteAsync(TId Id);
        Task SaveChangesAsync();
    }
}
