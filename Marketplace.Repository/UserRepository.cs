using AutoMapper;
using Marketplace.Contracts.Repository;
using Marketplace.DB;
using Marketplace.DB.Models;
using Marketplace.DTO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Repository
{
    public class UserRepository : BaseRepository<User, UserDto, Guid>, IUserRepository
    {
        public UserRepository(DataContext context, IMapper mapper) : base(context, mapper)
        { }

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

        public async Task UpdatePasswordAsync(Guid id, string oldPassword, string newPassword)
        {
            //if (user == null)
            //{
            //    throw new ArgumentNullException(nameof(user));
            //}

            //var findId = await _context.Users.FindAsync(id);

            //if (findId == null)
            //{
            //    throw new ArgumentNullException(nameof(findId));
            //}

            User user = await _userManager.FindByIdAsync($"{id}");
            if (user != null)
            {
                IdentityResult result =
                    await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

                if (result.Succeeded)
                {
                    return;
                }
                else
                {
                    throw new Exception("Не верный старый пароль");
                }
            }
            else
            {
                throw new Exception("Пользователь не найден");
            }
        }

    }
}