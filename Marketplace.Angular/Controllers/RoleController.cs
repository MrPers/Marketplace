//using AutoMapper;
//using Marketplace.Contracts.Services;
//using Marketplace.DTO.Models;
//using Marketplace.Web.Models;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Marketplace.Angular.Controllers
//{
//    [Route("")]
//    [ApiController]
//    public class RoleController : Controller
//    {
//        private readonly IRoleService _roleService;
//        private readonly IMapper _mapper;

//        public RoleController(
//            IRoleService roleService,
//            IMapper mapper
//        )
//        {
//            _roleService = roleService;
//            _mapper = mapper;
//        }

//        [HttpGet("get-role-all")]
//        public async Task<IActionResult> GetRolesAll()
//        {
//            var roles = await _roleService.GetAllAsync();
//            IActionResult result = roles == null ? NotFound() : Ok(_mapper.Map<List<RoleVM>>(roles));

//            return result;
//        }

//        [HttpGet("get-role-by-id/{id}")]
//        public async Task<IActionResult> GetRoleById(Guid id)
//        {
//            var roles = await _roleService.GetByIdAsync(id);
//            var rolesResult = _mapper.Map<RoleVM>(roles);
//            IActionResult result = roles == null ? NotFound() : Ok(rolesResult);

//            return result;
//        }

//        [HttpPost("add-role")]
//        public async Task<IActionResult> AddAsync(RoleVM role)
//        {
//            await _roleService.AddAsync(_mapper.Map<RoleDto>(role));

//            return Ok(true);
//        }

//        [HttpPut("update-role")]
//        public async Task<IActionResult> UpdateAsync(RoleVM role)
//        {
//            await _roleService.UpdateAsync(_mapper.Map<RoleDto>(role));

//            return Ok(true);
//        }

//        [HttpDelete("delete-role/{id}")]
//        public async Task<IActionResult> DeleteRole(Guid id)
//        {
//            await _roleService.DeleteAsync(id);

//            return Ok(true);
//        }
//    }
//}
