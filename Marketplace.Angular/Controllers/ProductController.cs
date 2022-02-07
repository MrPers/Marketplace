﻿using AutoMapper;
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
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(
            IProductService productService,
            IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        //[Authorize]
        [HttpGet("get-product-all")]
        public async Task<IActionResult> GetProductsAll()
        {
            var products = await _productService.GetAllAsync();
            IActionResult result = products == null ? NotFound() : Ok(_mapper.Map<List<BriefProductVM>>(products));

            return result;
        }

        [HttpGet("get-product-by-id/{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var products = await _productService.GetByIdAsync(id);
            var productsResult = _mapper.Map<FullProductVM>(products);
            IActionResult result = products == null ? NotFound() : Ok(productsResult);

            return result;
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> AddAsync(FullProductVM product)
        {
            await _productService.AddAsync(_mapper.Map<FullProductDto>(product));

            return Ok(true);
        }

        [HttpPut("update-product")]
        public async Task<IActionResult> UpdateAsync(FullProductVM product)
        {
            await _productService.UpdateAsync(_mapper.Map<FullProductDto>(product));

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