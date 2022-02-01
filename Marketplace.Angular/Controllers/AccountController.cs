using AutoMapper;
using IdentityServer4.Services;
using Marketplace.Angular.Models;
using Marketplace.Contracts.Services;
using Marketplace.DB;
using Marketplace.DB.Models;
using Marketplace.DTO.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Marketplace.Angular.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IIdentityServerInteractionService _interactionService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        //private readonly RoleManager<Role> _roleManager;
        //private readonly DataContext _context;

        public AccountController(
            IUserService userService,
            IMapper mapper,
            IIdentityServerInteractionService interactionService,
            IHttpClientFactory httpClientFactory,
            UserManager<User> userManager,
            //DataContext context,
            //RoleManager<Role> roleManager)
            SignInManager<User> signInManager)
        {
            _userService = userService;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            //_context = context;
            _interactionService = interactionService;
            _signInManager = signInManager;
            //_roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Login(string returnUrl)
        {
            //var uiRezult = _httpClientFactory.CreateClient();

            //var response = await uiRezult.GetAsync("http://localhost:4200/auth/login");

            return Ok(returnUrl);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] UserVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                ModelState.AddModelError("UserName", "User not found");
                return View(model);
            }

            var signinResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (signinResult.Succeeded)
            {
                return Ok();
            }
            ModelState.AddModelError("UserName", "Something went wrong");
            return View(model);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var result = await _interactionService.GetLogoutContextAsync(logoutId);

            return Redirect(result.PostLogoutRedirectUri);
        }

        [HttpPost("[action]")]
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