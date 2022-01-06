using AutoMapper;
using Marketplace.Contracts.Services;
using Marketplace.DTO.Models;
using Marketplace.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Marketplace.WebApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private ILogger<UserController> _logger;

        public UserController(
            IUserService userService,
            ILogger<UserController> logger,
            IMapper mapper
        )
        {
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
            //_logger.LogInformation("Initialization");
        }

        [HttpGet("get-users-all")]
        public async Task<IActionResult> GetUsersAll()
        {
            var users = await _userService.GetAllAsync();
            IActionResult result = users == null ? NotFound() : Ok(_mapper.Map<List<UserVM>>(users));

            return result;
        }

        [HttpGet("get-user-by-id/{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var users = await _userService.GetByIdAsync(id);
            var usersResult = _mapper.Map<UserVM>(users);
            IActionResult result = users == null ? NotFound() : Ok(usersResult);

            return result;
        }

        [HttpPost("add-user")]
        public async Task<IActionResult> AddAsync(UserVM user)
        {
            await _userService.AddAsync(_mapper.Map<UserDto>(user));

            return Ok(true);
        }

        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser(UserVM user)
        {
            await _userService.UpdateAsync(user.Id, _mapper.Map<UserDto>(user));

            return Ok(true);
        }

        [HttpDelete("delete-user/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteAsync(id);

            return Ok(true);
        }
    }
}
