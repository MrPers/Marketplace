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
    public class CartRepository : BaseRepository<Cart, CartDto, Guid>, ICartRepository
    {
        public CartRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {}

    }
}
