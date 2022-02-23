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
    public class UserChoiceRepository : BaseRepository<UserChoice, UserChoiceDto, Guid>, IUserChoiceRepository
    {
        public UserChoiceRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {}

        public async Task<ICollection<UserChoiceDto>> GetUsersChoiceByUserIdAsync(Guid Id)
        {
            var UserChoices = await _context.UserChoices
                .Where(u => u.UserId == Id)
                .Join(_context.StatusUserChoices,
                 p => p.StatusUserChoiceId,
                 t => t.Id,
                 (p, t) => new
                 {
                     UserChoiceId = p.Id,
                     NumberProductinUserChoice = p.NumberProduct,
                     StatusUserChoiceId = p.Id,
                     StatusUserChoiceName = t.Name,
                     PriceId = p.PriceId,
                 })
                .Join(_context.Prices,
                 p => p.PriceId,
                 t => t.Id,
                 (p, t) => new
                 {
                     UserChoiceId = p.UserChoiceId,
                     NumberProductinUserChoice = p.NumberProductinUserChoice,
                     StatusUserChoiceId = p.StatusUserChoiceId,
                     StatusUserChoiceName = p.StatusUserChoiceName,
                     PriceId = p.PriceId,
                     ProductId = t.ProductId,
                     ShopId = t.ShopId,
                     NumberProductAll = t.NumberProduct,
                     NetPrice = t.NetPrice,
                 })
                 .Join(_context.Shops,
                 p => p.ShopId,
                 t => t.Id,
                 (p, t) => new
                 {
                     UserChoiceId = p.UserChoiceId,
                     NumberProductinUserChoice = p.NumberProductinUserChoice,
                     StatusUserChoiceId = p.StatusUserChoiceId,
                     StatusUserChoiceName = p.StatusUserChoiceName,
                     PriceId = p.PriceId,
                     ProductId = p.ProductId,
                     ShopId = p.ShopId,
                     NumberProductAll = p.NumberProductAll,
                     NetPrice = p.NetPrice,
                     ShopName = t.Name,
                 })
                 .Join(_context.Products,
                 p => p.ProductId,
                 t => t.Id,
                 (p, t) => new UserChoiceDto
                 {
                     UserChoiceId = p.UserChoiceId,
                     NumberProductinUserChoice = p.NumberProductinUserChoice,
                     StatusUserChoiceId = p.StatusUserChoiceId,
                     StatusUserChoiceName = p.StatusUserChoiceName,
                     PriceId = p.PriceId,
                     ProductId = p.ProductId,
                     ShopId = p.ShopId,
                     NumberProductAll = p.NumberProductAll,
                     NetPrice = p.NetPrice,
                     ShopName = p.ShopName,
                     ProductName = t.Name,
                     ProductPhoto = t.Photo,
                 })
                .ToListAsync();

            return UserChoices;
        }

    }
}
