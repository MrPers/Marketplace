using AutoMapper;
using Marketplace.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Marketplace.DTO.Models;
using Marketplace.Models;

namespace Marketplace.Controllers
{
    [Route("")]
    [ApiController]
    public class UserChoiceController : Controller
    {
        private readonly ICommentProductService _commentService;
        private readonly IProductGroupService _productGroupService;
        private readonly IProductService _productService;
        private readonly IShopService _shopService;
        private readonly IPriceService _priceService;
        private readonly IUserChoiceService _userChoiceService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserChoiceController(
            IUserChoiceService userChoiceService,
            ICommentProductService commentService,
            IProductGroupService productGroupService,
            IProductService productService,
            IShopService shopService,
            IPriceService priceService,
            IUserService userService,
            IMapper mapper
        ){
            _userChoiceService = userChoiceService;
            _commentService = commentService;
            _userService = userService;
            _productGroupService = productGroupService;
            _productService = productService;
            _shopService = shopService;
            _priceService = priceService;
            _mapper = mapper;
        }

        [HttpGet("get-userChoice-by-user-id/{id}")]
        public async Task<IActionResult> GetUserChoiceByUserId(Guid id)
        {
            var userChoices = await _userChoiceService.GetUsersChoiceByUserIdAsync(id);
            var userChoicesResult = _mapper.Map<List<UserChoiceVM>>(userChoices);
            IActionResult result = userChoices == null ? NotFound() : Ok( new { userChoicesResult });

            return result;
        }

        [HttpGet("get-userChoice-all")]
        public async Task<IActionResult> GetUserChoicesAll()
        {
            var userChoices = await _userChoiceService.GetAllAsync();
            IActionResult result = userChoices == null ? NotFound() : Ok(_mapper.Map<List<UserChoiceVM>>(userChoices));

            return result;
        }

        [HttpGet("get-userChoice-by-id/{id}")]
        public async Task<IActionResult> GetUserChoiceById(Guid id)
        {
            var userChoices = await _userChoiceService.GetByIdAsync(id);
            var userChoicesResult = _mapper.Map<UserChoiceVM>(userChoices);
            IActionResult result = userChoices == null ? NotFound() : Ok(userChoicesResult);

            return result;
        }

        [HttpPost("add-userChoice")]
        public async Task<IActionResult> AddAsync(UserChoiceVM userChoice)
        {
            await _userChoiceService.AddAsync(_mapper.Map<UserChoiceDto>(userChoice));

            return Ok(true);
        }

        [HttpPut("update-userChoice")]
        public async Task<IActionResult> UpdateAsync(UserChoiceVM userChoice)
        {
            await _userChoiceService.UpdateAsync(_mapper.Map<UserChoiceDto>(userChoice));

            return Ok(true);
        }

        [HttpDelete("delete-userChoice/{id}")]
        public async Task<IActionResult> DeleteUserChoice(Guid id)
        {
            await _userChoiceService.DeleteAsync(id);

            return Ok(true);
        }
    }
}
