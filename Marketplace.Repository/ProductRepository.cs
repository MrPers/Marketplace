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
    public class ProductRepository : BaseRepository<Product, FullProductDto, Guid>, IProductRepository
    {
        public ProductRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {}

        public override async Task<ICollection<FullProductDto>> GetAllAsync()
        {
            var productsDto = await _context.Products.Join(_context.Prices,
             p => p.Id,
             t => t.ProductId,
             (p, t) => new FullProductDto
             {
                 Name = p.Name,
                 Id = p.Id,
                 Photo = p.Photo,
                 NetPrice = t.NetPrice,
             }).ToListAsync();

            return productsDto;
        }

        public override async Task<FullProductDto> GetByIdAsync(Guid id)
        {
            var productsDto = await _context.Products                
                .Join(_context.ProductGroups,
                 p => p.ProductGroupId,
                 t => t.Id,
                 (p, t) => new FullProductDto
                 {
                     Name = p.Name,
                     Id = p.Id,
                     Photo = p.Photo,
                     Description = p.Description,
                     ProductGroupId = p.ProductGroupId,
                     ProductGroupName = t.Name,
                 })
                .Where(p => p.Id == id)
                .FirstAsync();

            return productsDto;
        }
    }
}
