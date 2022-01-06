using Marketplace.Contracts.Repository;
using Marketplace.Contracts.Services;
using Marketplace.DTO.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Service
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ICollection<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return users;
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            return user;
        }

        public async Task AddAsync(UserDto user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await _userRepository.AddAsync(user);
        }

        public async Task DeleteAsync(Guid id)
        {

            await _userRepository.DeleteAsync(id);
        }

        public async Task UpdateAsync(Guid id, UserDto user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await _userRepository.UpdateAsync(id, user);
        }

    }
}
