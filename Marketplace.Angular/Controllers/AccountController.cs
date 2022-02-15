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
using System.ComponentModel.DataAnnotations;
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

        [HttpGet("[action]")]
        public IActionResult Login(string returnUrl)
        {
            //var externalProviders = await _signInManager.GetExternalAuthenticationSchemesAsync();
            var returnUrlRemove = this.Request.QueryString.Value.Remove(0, 11);
            return Redirect("https://localhost:5001/auth/login/:" + returnUrlRemove);
            //return View(new UserVM
            //{
            //    ReturnUrl = returnUrl,
            //    ExternalProviders = externalProviders
            //});
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserVM model)
        {
            var context = await _interactionService.GetAuthorizationContextAsync(model.ReturnUrl);

            var user = await _userManager.FindByNameAsync(model.UserName);//надо заменить

            if (user == null)
            {
                return View(model);
            }
            //"https://localhost:5001/" + model.ReturnUrl.Remove(0, 2)
            var signinResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (user != null && context != null)
            {
                //await HttpContext.SignInAsync(user.SubjectId, user.Username);
                return new JsonResult(new { RedirectUrl = model.ReturnUrl, IsOk = true });
            }

            return Unauthorized();
        }

        [HttpPost("authentication")]
        public async Task<IActionResult> Authentication([FromBody] UserVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.UserName);//надо заменить

            if (user == null)
            {
                return View(model);
            }
            //"https://localhost:5001/" + model.ReturnUrl.Remove(0, 2)
            var signinResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (signinResult.Succeeded)
            {
                return Ok(true);
            }

            return View(model);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var result = await _interactionService.GetLogoutContextAsync(logoutId);

            return Redirect(result.PostLogoutRedirectUri);
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