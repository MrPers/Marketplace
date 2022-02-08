using AutoMapper;
using Marketplace.Contracts.Repository;
using Marketplace.DB;
using Marketplace.DB.Models;
using Marketplace.DTO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Repository
{
    public class PriceRepository : BaseRepository<Price, PriceDto, Guid>, IPriceRepository
    {
        public PriceRepository(DataContext context, IMapper mapper) : base(context, mapper)
        { }

        public async Task<ICollection<PriceDto>> GetByProductIdAsync(Guid id)
        {
            var productsDto = await _context.Prices   
                .Where(p => p.ProductId == id)
                .Join(_context.Shops,
                 p => p.ShopId,
                 t => t.Id,
                 (p, t) => new PriceDto
                 {
                     Id = p.ShopId,
                     ShopName = t.Name,
                     NetPrice = p.NetPrice,
                     NumberProduct = p.NumberProduct,
                 })
                .ToListAsync();

            return productsDto;
        }
    }
}
