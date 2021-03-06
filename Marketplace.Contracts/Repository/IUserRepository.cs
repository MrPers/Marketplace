using Marketplace.DB.Models;
using Marketplace.DTO.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Contracts.Repository
{
    public interface IUserRepository : IBaseRepository<User, UserDto, Guid>
    {
        Task UpdatePasswordAsync(Guid id, string oldPassword, string newPassword);
        //Task<IdentityResult> Registration(User user);
        //Task SubscriptionToGroupsAsync(Guid groupId, Guid userId);
        //Task UnsubscriptionToGroupsAsync(Guid groupId, Guid userId);
    }
}
