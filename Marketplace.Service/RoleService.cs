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
    public class RoleService : IRoleService
    {
        private IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<ICollection<RoleDto>> GetAllAsync()
        {
            var roles = await _roleRepository.GetAllAsync();

            return roles;
        }

        public async Task<RoleDto> GetByIdAsync(Guid id)
        {
            var role = await _roleRepository.GetByIdAsync(id);

            return role;
        }

        public async Task AddAsync(RoleDto role)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            await _roleRepository.AddAsync(role);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _roleRepository.DeleteAsync(id);
        }

        public async Task UpdateAsync(RoleDto role)
        {
            await _roleRepository.UpdateAsync(role.Id, role);
        }
    }
}
