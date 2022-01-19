using AutoMapper;
using Marketplace.Contracts.Repository;
using Marketplace.DB;
using Marketplace.DB.Models;
using Marketplace.DTO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Repository
{
    public class ProductRepository : BaseRepository<Product, ProductDto, Guid>, IProductRepository
    {
        public ProductRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {}

        public override async Task<ProductDto> GetByIdAsync(Guid id)
        {
            var baseResult = await base.GetByIdAsync(id);
            //var productGroup = await _context.Set<ProductGroup>().FindAsync(baseResult.ProductGroupID);
            //baseResult.ProductGroupName = productGroup.Name;
            return baseResult;
        }
    }
}
