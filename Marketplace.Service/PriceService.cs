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
    public class PriceService : IPriceService
    {
        private IPriceRepository _priceRepository;

        public PriceService(IPriceRepository priceRepository)
        {
            _priceRepository = priceRepository;
        }

        public async Task<ICollection<PriceDto>> GetAllAsync()
        {
            var prices = await _priceRepository.GetAllAsync();

            return prices;
        }

        public async Task<PriceDto> GetByIdAsync(Guid id)
        {
            var price = await _priceRepository.GetByIdAsync(id);

            return price;
        }

        public async Task AddAsync(PriceDto price)
        {
            if (price == null)
            {
                throw new ArgumentNullException(nameof(price));
            }

            await _priceRepository.AddAsync(price);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _priceRepository.DeleteAsync(id);
        }

        public async Task UpdateAsync(PriceDto price)
        {
            await _priceRepository.UpdateAsync(price.Id, price);
        }

    }
}
