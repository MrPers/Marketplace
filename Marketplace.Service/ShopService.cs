using Marketplace.Contracts.Repository;
using Marketplace.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marketplace.Contracts.Services;

namespace Marketplace.Service
{
    class ShopService : IShopService
    {
        private IShopRepository _shopRepository;

        public ShopService(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        public async Task<ICollection<ShopDto>> GetAllAsync()
        {
            var shops = await _shopRepository.GetAllAsync();

            return shops;
        }

        public async Task<ShopDto> GetByIdAsync(Guid id)
        {
            var shop = await _shopRepository.GetByIdAsync(id);

            return shop;
        }

        public async Task AddAsync(ShopDto shop)
        {
            if (shop == null)
            {
                throw new ArgumentNullException(nameof(shop));
            }

            await _shopRepository.AddAsync(shop);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _shopRepository.DeleteAsync(id);
        }

        public async Task UpdateAsync(ShopDto shop)
        {
            await _shopRepository.UpdateAsync(shop.Id, shop);
        }
    }
}
