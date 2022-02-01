using AutoMapper;
using Marketplace.Angular.Models;
using Marketplace.Contracts.Services;
using Marketplace.DTO.Models;
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
        private readonly IUserService _userService;
        private readonly IShopService _shopService;
        private readonly IPriceService _priceService;
        private readonly IProductService _productService;
        private readonly IProductGroupService _productGroupService;
        private readonly ICommentProductService _commentProductService;
        private readonly IMapper _mapper;

        public ProductController(
            IUserService userService,
            IProductService productService,
            IPriceService priceService, 
            IShopService shopService,
            IProductGroupService productGroupService,
            ICommentProductService commentProductService,
            IMapper mapper)
        {
            _userService = userService;
            _productGroupService = productGroupService;
            _commentProductService = commentProductService;
            _productService = productService;
            _shopService = shopService;
            _priceService = priceService;
            _mapper = mapper;
        }

        [HttpGet("get-all-product-price-shop")]
        public async Task<IActionResult> GetAllProductPriceShop()
        {
            var products = await _productService.GetAllAsync();
            var users = await _userService.GetAllAsync();
            var shops = await _shopService.GetAllAsync();
            var prices = await _priceService.GetAllAsync();
            var productGroups = await _productGroupService.GetAllAsync();
            var commentProducts = await _commentProductService.GetAllAsync();

            var productsResult = _mapper.Map<List<ProductVM>>(products);
            var shopsResult = _mapper.Map<List<ShopVM>>(shops);
            var pricesResult = _mapper.Map<List<PriceVM>>(prices);
            var productGroupsResult = _mapper.Map<List<ProductGroupVM>>(productGroups);
            var commentProductsResult = _mapper.Map<List<CommentProductVM>>(commentProducts);
            var usersResult = _mapper.Map<List<UserVM>>(users);
            var time = new { productsResult, shopsResult, pricesResult, productGroupsResult, commentProductsResult, usersResult };
            IActionResult result = products == null ? NotFound() : Ok(time);

            return result;
        }

        //[Authorize]
        [HttpGet("get-product-all")]
        public async Task<IActionResult> GetProductsAll()
        {
            var products = await _productService.GetAllAsync();
            IActionResult result = products == null ? NotFound() : Ok(_mapper.Map<List<ProductVM>>(products));

            return result;
        }

        [HttpGet("get-product-by-id/{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var products = await _productService.GetByIdAsync(id);
            var productsResult = _mapper.Map<ProductVM>(products);
            IActionResult result = products == null ? NotFound() : Ok(productsResult);

            return result;
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> AddAsync(ProductVM product)
        {
            await _productService.AddAsync(_mapper.Map<ProductDto>(product));

            return Ok(true);
        }

        [HttpPut("update-product")]
        public async Task<IActionResult> UpdateAsync(ProductVM product)
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
