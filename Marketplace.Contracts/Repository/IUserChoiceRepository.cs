using Marketplace.DB.Models;
using Marketplace.DTO.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Contracts.Repository
{
    public interface IUserChoiceRepository : IBaseRepository<UserChoice, UserChoiceDto, Guid>
    {
        Task<ICollection<UserChoiceDto>> GetUsersChoiceByUserIdAsync(Guid Id);

        //Task SubscriptionToGroupsAsync(Guid groupId, Guid userId);
        //Task UnsubscriptionToGroupsAsync(Guid groupId, Guid userId);
    }
}
