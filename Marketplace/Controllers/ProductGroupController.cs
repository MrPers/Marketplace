using AutoMapper;
using Marketplace.Models;
using Marketplace.Contracts.Services;
using Marketplace.DTO.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Controllers
{
    [Route("")]
    [ApiController]
    public class ProductGroup : Controller
    {
        private readonly IProductGroupService _productGroupService;
        private readonly IMapper _mapper;

        public ProductGroup(
            IProductGroupService productGroupService,
            IMapper mapper
        )
        {
            _productGroupService = productGroupService;
            _mapper = mapper;
        }

        [HttpGet("get-productGroup-all")]
        public async Task<IActionResult> GetProductGroupsAll()
        {
            var productGroups = await _productGroupService.GetAllAsync();
            IActionResult result = productGroups == null ? NotFound() : Ok(_mapper.Map<List<ProductGroupVM>>(productGroups));

            return result;
        }

        [HttpGet("get-productGroup-by-id/{id}")]
        public async Task<IActionResult> GetProductGroupById(Guid id)
        {
            var productGroups = await _productGroupService.GetByIdAsync(id);
            var productGroupsResult = _mapper.Map<ProductGroupVM>(productGroups);
            IActionResult result = productGroups == null ? NotFound() : Ok(productGroupsResult);

            return result;
        }

        [HttpPost("add-productGroup")]
        public async Task<IActionResult> AddAsync(ProductGroupVM productGroup)
        {
            await _productGroupService.AddAsync(_mapper.Map<ProductGroupDto>(productGroup));

            return Ok(true);
        }

        [HttpPut("update-productGroup")]
        public async Task<IActionResult> UpdateAsync(ProductGroupVM productGroup)
        {
            await _productGroupService.UpdateAsync(_mapper.Map<ProductGroupDto>(productGroup));

            return Ok(true);
        }

        [HttpDelete("delete-productGroup/{id}")]
        public async Task<IActionResult> DeleteProductGroup(Guid id)
        {
            await _productGroupService.DeleteAsync(id);

            return Ok(true);
        }
    }
}