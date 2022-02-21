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
    public class ShopController : Controller
    {
        private readonly IShopService _shopService;
        private readonly IMapper _mapper;

        public ShopController(
            IShopService shopService,
            IMapper mapper
        )
        {
            _shopService = shopService;
            _mapper = mapper;
        }

        [HttpGet("get-shop-all")]
        public async Task<IActionResult> GetShopsAll()
        {
            var shops = await _shopService.GetAllAsync();
            IActionResult result = shops == null ? NotFound() : Ok(_mapper.Map<List<ShopVM>>(shops));

            return result;
        }

        [HttpGet("get-shop-by-id/{id}")]
        public async Task<IActionResult> GetShopById(Guid id)
        {
            var shops = await _shopService.GetByIdAsync(id);
            var shopsResult = _mapper.Map<ShopVM>(shops);
            IActionResult result = shops == null ? NotFound() : Ok(shopsResult);

            return result;
        }

        [HttpPost("add-shop")]
        public async Task<IActionResult> AddAsync(ShopVM shop)
        {
            await _shopService.AddAsync(_mapper.Map<ShopDto>(shop));

            return Ok(true);
        }

        [HttpPut("update-shop")]
        public async Task<IActionResult> UpdateAsync(ShopVM shop)
        {
            await _shopService.UpdateAsync(_mapper.Map<ShopDto>(shop));

            return Ok(true);
        }

        [HttpDelete("delete-shop/{id}")]
        public async Task<IActionResult> DeleteShop(Guid id)
        {
            await _shopService.DeleteAsync(id);

            return Ok(true);
        }
    }
}