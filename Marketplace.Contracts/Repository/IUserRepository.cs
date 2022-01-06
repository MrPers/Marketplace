using Marketplace.DB.Models;
using Marketplace.DTO.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Marketplace.Contracts.Repository
{
    public interface IUserRepository : IBaseRepository<User, UserDto, Guid>
    {
        //override async Task<Guid> AddAsync(User user);
        //Task<IdentityResult> Registration(User user);
        //Task SubscriptionToGroupsAsync(Guid groupId, Guid userId);
        //Task UnsubscriptionToGroupsAsync(Guid groupId, Guid userId);
    }
}
