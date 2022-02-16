using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Marketplace.DB;
using Marketplace.DB.Models;
using Microsoft.AspNetCore.Identity;

namespace Marketplace.Angular.Infrastructure
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

        //public Task GetProfileDataAsync(ProfileDataRequestContext context)
        //public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);

            //1)
            var claims = new List<System.Security.Claims.Claim>
            {
              new System.Security.Claims.Claim("name", user.UserName),
            };
            //        //2)
            //        var claims = context.Subject.FindAll(JwtClaimTypes.Name);
            //        //3)

            //var time = await _context.Roles
            //    .Join(_context.UserRoles,
            //     p => p.Id,
            //     t => t.RoleId,
            //     (p, t) => new
            //     {
            //         Name = p.Name,
            //         Id = p.Id,
            //         UserId = t.UserId,
            //     })
            //    .Join(_context.UserRoleShops,
            //     p => p.Id,
            //     t => t.RoleId,
            //     (p, t) => new
            //     {
            //         Name = p.Name,
            //         Id = t.UserId,
            //     })
            //    .Join(_context.Users,
            //     p => p.Id,
            //     t => t.Id,
            //     (p, t) => new
            //     {
            //         Name = p.Name,
            //         Id = t.Id,
            //     })
            //    .Where(x => x.Id == context.Subject.Identity.Name).ToList()[0].UserName;

            //        //4)
            //        context.Subject.Identity.Name

            //var claims = new List<System.Security.Claims.Claim>();
            //var scops = context.Subject.FindAll(JwtClaimTypes.Scope).ToList();
            //foreach (var item in scops)
            //{
            //    claims.Add(new System.Security.Claims.Claim(item.Value, "True"));
            //}

            //claims.Add(context.Subject.FindFirst(JwtClaimTypes.Role));

            context.IssuedClaims = claims;




            //var claims = _context.Claims.ToList();
            //claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Name)).ToList();

            //if (user.JobTitle != null)
            //    claims.Add(new System.Security.Claims.Claim(PropertyConstants.JobTitle, user.JobTitle));

            //if (user.FullName != null)
            //    claims.Add(new Claim(PropertyConstants.FullName, user.FullName));

            //if (user.Configuration != null)
            //    claims.Add(new Claim(PropertyConstants.Configuration, user.Configuration));

            //context.IssuedClaims = claims;
        }

        // This method gets called whenever identity server needs to determine if the user is valid or active (e.g. if the user's account has been deactivated since they logged in).
        //public Task IsActiveAsync(IsActiveContext context)
        public async Task IsActiveAsync(IsActiveContext context)
        {

            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);

            context.IsActive = (user != null);
        }
    }
}
