using AutoMapper;
using Marketplace.Contracts.Repository;
using Marketplace.DB;
using Marketplace.DB.Models;
using Marketplace.DTO.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Repository
{
    public class UserRepository : BaseRepository<User, UserDto, Guid>, IUserRepository
    {
        public UserRepository(
            DataContext context, 
            IMapper mapper,
            UserManager<User> userManager,
            RoleManager<Role> roleManager) : base(context, mapper, userManager, roleManager)
        {
        }

        public override async Task<Guid> AddAsync(UserDto user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var result = _userManager
                .CreateAsync(_mapper.Map<User>(user), user.Password)
                .GetAwaiter()
                .GetResult();

            if (result.Succeeded)
            {
                return await _context.Users.MaxAsync(p => p.Id);
            }

            throw new Exception(result.Succeeded.ToString());
        }

        public override async Task UpdateAsync(Guid id, UserDto user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var findId = _context.Users.Find(id);

            if (findId == null)
            {
                throw new ArgumentNullException(nameof(findId));
            }

            var result = _userManager
                .UpdateAsync(_mapper.Map<User>(user))
                .GetAwaiter()
                .GetResult();

            if (result.Succeeded)
            {
                return;
            }

            throw new Exception(result.Succeeded.ToString());
        }

    }
}