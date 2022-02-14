using AutoMapper;
using Marketplace.Angular.Models;
using Marketplace.Contracts.Services;
using Marketplace.DTO.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Angular.Controllers
{
    [Route("")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ICommentProductService _commentService;
        private readonly IProductGroupService _productGroupService;
        private readonly IProductService _productService;
        private readonly IShopService _shopService;
        private readonly IPriceService _priceService;
        private readonly IMapper _mapper;

        public ProductController(
            IProductGroupService productGroupService,
            ICommentProductService commentService,
            IProductService productService,
            IShopService shopService,
            IPriceService priceService,
            IMapper mapper)
        {
            _productGroupService = productGroupService;
            _shopService = shopService;
            _priceService = priceService;
            _commentService = commentService;
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet("get-product-all")]
        public async Task<IActionResult> GetProductsAll()
        {
            var products = await _productService.GetAllAsync();
            var productGroups = await _productGroupService.GetAllAsync();
            var productsResult = _mapper.Map<List<BriefProductVM>>(products);
            var productGroupResult = _mapper.Map<List<ProductGroupVM>>(productGroups);

            IActionResult result = productGroups == null ? NotFound() : Ok(new { productsResult, productGroupResult });

            return result;
        }

        [HttpGet("get-product-by-shop-id/{id}")]
        public async Task<IActionResult> GetProductByShopId(Guid id)
        {
            var products = await _productService.GetProductByShopId(id);
            var productGroups = await _productGroupService.GetAllAsync(); 
            var shop = await _shopService.GetByIdAsync(id);
            var shopsResult = _mapper.Map<ShopVM>(shop);
            var productsResult = _mapper.Map<List<BriefProductVM>>(products);
            var productGroupResult = _mapper.Map<List<ProductGroupVM>>(productGroups);

            IActionResult result = productGroups == null ? NotFound() : Ok(new { productsResult, productGroupResult, shopsResult});

            return result;
        }

        [HttpGet("get-product-by-id/{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var comments = await _commentService.GetByProductIdAsync(id);
            var prices = await _priceService.GetByProductIdAsync(id);
            var products = await _productService.GetByIdAsync(id);
            var productsResult = _mapper.Map<FullProductVM>(products);
            var priceResult = _mapper.Map<List<PriceVM>>(prices);
            var commentsResult = _mapper.Map<List<CommentProductVM>>(comments);
            IActionResult result = products == null ? NotFound() : Ok(new { productsResult, commentsResult, priceResult});

            return result;
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> AddAsync(FullProductVM product)
        {
            await _productService.AddAsync(_mapper.Map<ProductDto>(product));

            return Ok(true);
        }

        [HttpPut("update-product")]
        public async Task<IActionResult> UpdateAsync(FullProductVM product)
        {
            await _productService.UpdateAsync(_mapper.Map<ProductDto>(product));

            return Ok(true);
        }

        [HttpDelete("delete-product/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _productService.DeleteAsync(id);

            return Ok(true);
        }

    }
}
