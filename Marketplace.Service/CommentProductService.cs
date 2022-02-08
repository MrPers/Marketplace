using Marketplace.Contracts.Repository;
using Marketplace.Contracts.Services;
using Marketplace.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Service
{
    public class CommentProductService : ICommentProductService
    {
        private ICommentProductRepository _commentProductRepository;

        public CommentProductService(ICommentProductRepository commentProductRepository)
        {
            _commentProductRepository = commentProductRepository;
        }

        public async Task<ICollection<CommentProductDto>> GetAllAsync()
        {
            var commentProducts = await _commentProductRepository.GetAllAsync();

            return commentProducts;
        }

        public async Task<CommentProductDto> GetByIdAsync(Guid id)
        {
            var commentProduct = await _commentProductRepository.GetByIdAsync(id);

            return commentProduct;
        }

        public async Task AddAsync(CommentProductDto commentProduct)
        {
            if (commentProduct == null)
            {
                throw new ArgumentNullException(nameof(commentProduct));
            }

            await _commentProductRepository.AddAsync(commentProduct);
        }

        public async Task<ICollection<CommentProductDto>> GetByProductIdAsync(Guid id)
        {
            var commentsProduct = await _commentProductRepository.GetByProductIdAsync(id);

            return commentsProduct;
        }

        public async Task UpdateAsync(CommentProductDto commentProduct)
        {
            await _commentProductRepository.UpdateAsync(commentProduct.Id, commentProduct);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _commentProductRepository.DeleteAsync(id);
        }
    }
}
