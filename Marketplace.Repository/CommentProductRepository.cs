using AutoMapper;
using Marketplace.Contracts.Repository;
using Marketplace.DB;
using Marketplace.DB.Models;
using Marketplace.DTO.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Repository
{
    public class CommentProductRepository : BaseRepository<CommentProduct, CommentProductDto, Guid>, ICommentProductRepository
    {
        public CommentProductRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {}

        public async Task<ICollection<CommentProductDto>> GetByProductIdAsync(Guid id)
        {
            var productsDto = await _context.CommentProducts
                .Where(t => t.ProductId == id)
                .Join(_context.Users,
                 p => p.UserId,
                 t => t.Id,
                 (p, t) => new CommentProductDto
                 {
                     UserName = t.UserName,
                     Id = p.Id,
                     Text = p.Text,
                     ProductId = p.ProductId,
                     UserId = p.UserId,
                     DepartureDate = p.DepartureDate,
                 })
                .ToListAsync();

            return _mapper.Map<ICollection<CommentProductDto>>(productsDto);

        }
    }
}
