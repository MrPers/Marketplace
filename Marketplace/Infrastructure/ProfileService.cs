using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Marketplace.DB;
using Marketplace.DB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Infrastructure
{
    public class ProfileService : IProfileService   //позволяет динамически загружать доп данные, пример в Mail.UI юз.
    {
        protected UserManager<User> _userManager;
        protected DataContext _context;

        public ProfileService(DataContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            var userRoles = await _userManager.GetRolesAsync(user);
            var scops = context.Subject.FindAll(JwtClaimTypes.Scope).ToList();

            var claims = new List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim("name", user.UserName),
            };

            foreach (var item in scops)
            {
                claims.Add(new System.Security.Claims.Claim(item.Value, "True"));
            }

            foreach (var userRole in userRoles)
            {
                claims.Add(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, userRole));
            }

            context.IssuedClaims = claims;

        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            //var sub = context.Subject.GetSubjectId();
            //var user = await _userManager.FindByIdAsync(sub);

            //context.IsActive = (user != null);
        }
    }
}


//var time = await _context.Roles
//    .Join(_context.UserRoleShops,
//     p => p.Id,
//     t => t.RoleId,
//     (p, t) => new
//     {
//         Name = p.Name,
//         Id = p.Id,
//     })
//    .Join(_context.Users,
//     p => p.Id,
//     t => t.Id,
//     (p, t) => new
//     {
//         Name = p.Name,
//         Id = t.Id,
//     })
//    .Where(x => x.Id == user.Id)
//    .Select(x => x.Name)
//    .ToListAsync();

//if(time.Count == 0)
//{
//    time = await _context.Roles
//        .Join(_context.UserRoles,
//         p => p.Id,
//         t => t.RoleId,
//         (p, t) => new
//         {
//             Name = p.Name,
//             Id = p.Id,
//         })
//        .Join(_context.Users,
//         p => p.Id,
//         t => t.Id,
//         (p, t) => new
//         {
//             Name = p.Name,
//             Id = t.Id,
//         })
//        .Where(x => x.Id == user.Id).Select(x => x.Name).ToListAsync();
//}

//foreach (var item in time)
//{
//claims.Add((System.Security.Claims.Claim)context.Subject.FindFirst(JwtClaimTypes.Role));
//claims.Add((System.Security.Claims.Claim)context.Subject.FindFirst("Owner"));

//claims.Add(new System.Security.Claims.Claim("role", "Owner"));
//}

