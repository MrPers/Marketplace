using Marketplace.Contracts.Repository;
using Marketplace.Contracts.Services;
using Marketplace.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Service
{
    public class UserChoiceService : IUserChoiceService
    {
        private IUserChoiceRepository _userChoiceRepository;

        public UserChoiceService(IUserChoiceRepository userChoiceRepository)
        {
            _userChoiceRepository = userChoiceRepository;
        }

        public async Task<ICollection<UserChoiceDto>> GetAllAsync()
        {
            var userChoices = await _userChoiceRepository.GetAllAsync();

            return userChoices;
        }

        public async Task<UserChoiceDto> GetByIdAsync(Guid id)
        {
            var userChoice = await _userChoiceRepository.GetByIdAsync(id);

            return userChoice;
        }

        public async Task AddAsync(UserChoiceDto userChoice)
        {
            if (userChoice == null)
            {
                throw new ArgumentNullException(nameof(userChoice));
            }

            await _userChoiceRepository.AddAsync(userChoice);
        }

        public async Task UpdateAsync(UserChoiceDto userChoice)
        {
            await _userChoiceRepository.UpdateAsync(userChoice.Id, userChoice);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _userChoiceRepository.DeleteAsync(id);
        }

        public async Task<ICollection<UserChoiceDto>> GetUsersChoiceByUserIdAsync(Guid id)
        {
            var userChoice = await _userChoiceRepository.GetUsersChoiceByUserIdAsync(id);

            return userChoice;
        }
    }
}
