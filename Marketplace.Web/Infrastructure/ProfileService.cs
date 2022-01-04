﻿using IdentityServer4.Models;
using IdentityServer4.Services;
using Marketplace.DB;
using Marketplace.DB.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Web.Infrastructure
{
    public class ProfileService : IProfileService   //позволяет динамически загружать доп данные, пример в Mail.UI юз.
    {
        protected UserManager<User> _userManager;
        protected readonly DataContext _context;

        public ProfileService(DataContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //public Task GetProfileDataAsync(ProfileDataRequestContext context)
        //public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        //{
        //    var user = await _userManager.GetUserAsync(context.Subject);

        //    ////1)
        //    //        var claims = new List<Claim>
        //    //{
        //    //            new Claim(ClaimTypes.Name, user.UserName),
        //    //};    
        //    //        //2)
        //    //        var claims = context.Subject.FindAll(JwtClaimTypes.Name);
        //    //        //3)
        //    //        var time = _context.Set<User>()
        //    //            .Where(x => x.UserName == context.Subject.Identity.Name).ToList()[0].UserName;
        //    //        //4)
        //    //        context.Subject.Identity.Name

        //    var claims = new List<Claim>();
        //    var scops = context.Subject.FindAll(JwtClaimTypes.Scope).ToList();
        //    foreach (var item in scops)
        //    {
        //        claims.Add(new Claim(item.Value, "True"));
        //    }

        //    claims.Add((Claim)context.Subject.FindFirst(JwtClaimTypes.Role));

        //    context.IssuedClaims.AddRange(claims);
        //}
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            context.IssuedClaims.Add(new System.Security.Claims.Claim("test-claim", "test-value"));
            return Task.FromResult(0);
        }

        // This method gets called whenever identity server needs to determine if the user is valid or active (e.g. if the user's account has been deactivated since they logged in).
        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.CompletedTask;
        }
    }
}