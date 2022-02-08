using Marketplace.DTO.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Contracts.Services
{
    public interface ICommentProductService
    {
        Task<ICollection<CommentProductDto>> GetAllAsync();
        Task<CommentProductDto> GetByIdAsync(Guid id);
        Task<ICollection<CommentProductDto>> GetByProductIdAsync(Guid id);
        Task AddAsync(CommentProductDto commentProduct);
        Task UpdateAsync(CommentProductDto commentProduct);
        Task DeleteAsync(Guid id);
    }
}
