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
    public class ProductRepository : BaseRepository<Product, ProductDto, Guid>, IProductRepository
    {
        public ProductRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {}

        public override async Task<ICollection<ProductDto>> GetAllAsync()
        {
            var productsDto = await _context.Products
                .Join(_context.Prices,
                 p => p.Id,
                 t => t.ProductId,
                 (p, t) => new ProductDto
                 {
                     Name = p.Name,
                     Id = p.Id,
                     Photo = p.Photo,
                     NetPrice = t.NetPrice,
                     ProductGroupId = p.ProductGroupId,
                 })
                .ToListAsync();

            return productsDto;
        }

        public override async Task<ProductDto> GetByIdAsync(Guid id)
        {
            var productsDto = await _context.Products                
                .Join(_context.ProductGroups,
                 p => p.ProductGroupId,
                 t => t.Id,
                 (p, t) => new ProductDto
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

        public async Task<ICollection<ProductDto>> GetProductByShopId(Guid id)
        {
            var productsDto = await _context.Prices
                .Where(p => p.ShopId == id)
                .Join(_context.Products,
                 t => t.ProductId,
                 p => p.Id,
                 (t, p) => new ProductDto
                 {
                     Name = p.Name,
                     Id = p.Id,
                     Photo = p.Photo,
                     NetPrice = t.NetPrice,
                     ProductGroupId = p.ProductGroupId,
                 })
                .ToListAsync();

            return productsDto;
        }

    }
}
