using Marketplace.DB.Models;
using Marketplace.DTO.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Contracts.Repository
{
    public interface ICommentProductRepository : IBaseRepository<CommentProduct, CommentProductDto, Guid>
    {
        Task<ICollection<CommentProductDto>> GetByProductIdAsync(Guid id);
        //Task SubscriptionToGroupsAsync(Guid groupId, Guid userId);
        //Task UnsubscriptionToGroupsAsync(Guid groupId, Guid userId);
    }
}
