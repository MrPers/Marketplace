using AutoMapper;
using IdentityServer4.Services;
using Marketplace.Models;
using Marketplace.Contracts.Services;
using Marketplace.DB.Models;
using Marketplace.DTO.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IIdentityServerInteractionService _interactionService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountController(
            IUserService userService,
            IMapper mapper,
            IIdentityServerInteractionService interactionService,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userService = userService;
            _mapper = mapper;
            _interactionService = interactionService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.UserName);//надо заменить

            if (user == null)
            {
                return Unauthorized();
            }

            var signinResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (user != null && signinResult != null)
            {
                return Ok();
            }

            return Unauthorized();
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserVM user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                await _userService.AddAsync(_mapper.Map<UserDto>(user));

                return StatusCode(201);
            }
            catch
            {
                return BadRequest();
            }                      
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

        [HttpPut("update-user-password")]
        public async Task<IActionResult> UpdatePasswordAsync(Guid id, string oldPassword, string newPassword)
        {
            await _userService.UpdatePasswordAsync(id, oldPassword, newPassword);

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