using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Marketplace.DB
{
    public class ItializerData
    {
        public static void Init(IServiceProvider scopeServiceProvider)
        {
            var context = scopeServiceProvider.GetRequiredService<ConfigurationDbContext>();

            if (!context.Clients.Any())
            {
                foreach (var client in IdentityServerConfiguration.GetClients())
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in IdentityServerConfiguration.GetIdentityResources())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var resource in IdentityServerConfiguration.GetApiResources())
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            var dataContext = scopeServiceProvider.GetRequiredService<DataContext>();

            if (!dataContext.Users.Any() || !dataContext.Products.Any() || !dataContext.Shops.Any())
            {
                //User[] users = new User[] {
                //     new User{ Name = "TestName1", Surname = "TestSurname1", Email = "iamanton45@gmail.com" },
                //     new User{ Name = "TestName2", Surname = "TestSurname2", Email = "iamanton@ukr.net" },
                //};

                //Group[] groups = new Group[] {
                //    new Group {Name = "Test Name"}
                //};

                //context.Groups.AddRange(groups);
                //context.Users.AddRange(users);

                //users[0].Groups.Add(groups[0]);

                //context.SaveChanges();
            }
        }
    }
}