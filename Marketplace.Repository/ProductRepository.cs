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

        public new async Task<ICollection<BriefProductDto>> GetAllAsync()
        {
            var productsDto = await _context.Products.Join(_context.Prices,
             p => p.Id,
             t => t.ProductId,
             (p, t) => new BriefProductDto
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
            //var query = await
            //    from t1 in _context.Products
            //    join t2 in _context.Prices
            //      on new { ColA = t1.Id, ColB = t1.Id } equals new { ColA = t2.Id, ColB = t2.Id }
            //    join t3 in _context.ProductGroups
            //      on new { ColC = t2.Id, ColD = t2.Id } equals new { ColC = t3.Name, ColD = t3.Products };


            var productsDto = await _context.Products
                .Join(_context.Prices,
                 p => p.Id,
                 t => t.ProductId,
                 (p, t) => new
                 {
                     Name = p.Name,
                     Id = p.Id,
                     Photo = p.Photo,
                     Description = p.Description,
                     NetPrice = t.NetPrice,
                     NumberProduct = t.NumberProduct,
                     ProductGroupId = p.ProductGroupId,
                     ShopId = t.ShopId,
                 })
                .Join(_context.ProductGroups,
                 p => p.ProductGroupId,
                 t => t.Id,
                 (p, t) => new
                 {
                     Name = p.Name,
                     Id = p.Id,
                     Photo = p.Photo,
                     Description = p.Description,
                     NetPrice = p.NetPrice,
                     NumberProduct = p.NumberProduct,
                     ProductGroupId = p.ProductGroupId,
                     ProductGroupName = t.Name,
                     ShopId = p.ShopId,
                 })
                .Join(_context.Shops,
                 p => p.ShopId,
                 t => t.Id,
                 (p, t) => new FullProductDto
                 {
                     Name = p.Name,
                     Id = p.Id,
                     Photo = p.Photo,
                     Description = p.Description,
                     NetPrice = p.NetPrice,
                     NumberProduct = p.NumberProduct,
                     ProductGroupId = p.ProductGroupId,
                     ProductGroupName = p.ProductGroupName,
                     ShopId = p.ShopId,
                     ShopName = t.Name,
                 })
                .Where(p => p.Id == id)
                .FirstAsync();

            return productsDto;
        }
    }
}
