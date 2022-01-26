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
    public class ProductGroupService : IProductGroupService
    {
        private IProductGroupRepository _productGroupRepository;

        public ProductGroupService(IProductGroupRepository productGroupRepository)
        {
            _productGroupRepository = productGroupRepository;
        }

        public async Task<ICollection<ProductGroupDto>> GetAllAsync()
        {
            var productGroups = await _productGroupRepository.GetAllAsync();

            return productGroups;
        }

        public async Task<ProductGroupDto> GetByIdAsync(Guid id)
        {
            var productGroup = await _productGroupRepository.GetByIdAsync(id);

            return productGroup;
        }

        public async Task AddAsync(ProductGroupDto productGroup)
        {
            if (productGroup == null)
            {
                throw new ArgumentNullException(nameof(productGroup));
            }

            await _productGroupRepository.AddAsync(productGroup);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _productGroupRepository.DeleteAsync(id);
        }

        public async Task UpdateAsync(ProductGroupDto productGroup)
        {
            await _productGroupRepository.UpdateAsync(productGroup.Id, productGroup);
        }

    }
}
