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
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ICollection<ProductDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();

            products = AveragePriceOutputProducts(products);

            return products;
        }

        public async Task<ProductDto> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            return product;
        }

        public async Task AddAsync(ProductDto product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            await _productRepository.AddAsync(product);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _productRepository.DeleteAsync(id);
        }

        public async Task UpdateAsync(ProductDto product)
        {
            await _productRepository.UpdateAsync(product.Id, product);
        }

        private static ICollection<ProductDto> AveragePriceOutputProducts(ICollection<ProductDto> products)
        {
            var duplicates = products   //повторяющееся значение по группам повторения
              .GroupBy(r => r.Id)
              .Where(g => g.Count() > 1)
              .ToList();

            foreach (var duplicate in duplicates)
            {
                var product = duplicate.Last(); //берем продукт который будем добавлять

                product.NetPrice = duplicate.Average(r => r.NetPrice);  //среднея цена этих товаров

                product.PricesAverage = true;  //указываем, что среднея цена указана

                products = products.Except(duplicate).ToList(); //удалить повторяющееся значение

                products.Add(product);
            }

            return products;
        }

    }
}
