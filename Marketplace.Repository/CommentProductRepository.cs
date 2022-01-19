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
    public class CommentProductRepository : BaseRepository<CommentProduct, CommentProductDto, Guid>, ICommentProductRepository
    {
        public CommentProductRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {}

    }
}
