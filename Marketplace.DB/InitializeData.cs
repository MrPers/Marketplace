using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Marketplace.DB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.DB
{
    public class InitializeData
    {
        public static async Task Init(IServiceProvider scopeServiceProvider)
        {
            //await scopeServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.MigrateAsync();

            //var bdContext = scopeServiceProvider.GetRequiredService<ConfigurationDbContext>();

            //if (!bdContext.Clients.Any())
            //{
            //    foreach (var client in IdentityServerConfiguration.GetClients())
            //    {
            //        bdContext.Clients.Add(client.ToEntity());
            //    }
            //    bdContext.SaveChanges();
            //}

            //if (!bdContext.IdentityResources.Any())
            //{
            //    foreach (var resource in IdentityServerConfiguration.GetIdentityResources())
            //    {
            //        bdContext.IdentityResources.Add(resource.ToEntity());
            //    }
            //    bdContext.SaveChanges();
            //}

            //if (!bdContext.ApiResources.Any())
            //{
            //    foreach (var resource in IdentityServerConfiguration.GetApiResources())
            //    {
            //        bdContext.ApiResources.Add(resource.ToEntity());
            //    }
            //    bdContext.SaveChanges();
            //}

            //await bdContext.Database.MigrateAsync();

            var dataContext = scopeServiceProvider.GetRequiredService<DataContext>();

            if (!dataContext.Users.Any() || !dataContext.Products.Any() || !dataContext.Shops.Any() || !dataContext.Roles.Any() || !dataContext.UserRoles.Any())
            {
                var userManager = scopeServiceProvider.GetService<UserManager<User>>();
                var roleManager = scopeServiceProvider.GetService<RoleManager<Role>>();

                Claim[] claims = new Claim[] {
                    new Claim{Name = "EditingAllUsers"},
                    new Claim{Name = "EditingAllStores"},
                    new Claim{Name = "EditingStore"},
                    new Claim{Name = "DeletingStore"},
                };

                dataContext.Claims.AddRange(claims);

                Role[] roles = new Role[] {
                    new Role("Owner"),
                    new Role("StoreAdministrator"),
                    new Role("PlatformAdministrator"),
                };

                roleManager.CreateAsync(roles[0]).GetAwaiter().GetResult();
                roleManager.CreateAsync(roles[1]).GetAwaiter().GetResult();
                roleManager.CreateAsync(roles[2]).GetAwaiter().GetResult();

                roles[0].Claims.Add(claims[2]);
                roles[0].Claims.Add(claims[3]);
                roles[1].Claims.Add(claims[2]);
                roles[2].Claims.Add(claims[0]);
                roles[2].Claims.Add(claims[1]);

                User[] users = new User[] {
                    new User("AdministratorAll"){Email = "AdmAll@ukr.net"},
                    new User("OwnerShop1Shop2"){Email = "Own1@ukr.net"},
                    new User("AdmShop1OwnerShop3"){Email = "Adm1@ukr.net"},
                    new User("AdmShop2"){Email = "Adm2@ukr.net"},
                    new User("AdmShop3"){Email = "Adm2@ukr.net"},
                    new User("User"){Email = "User@ukr.net"},
                };

                var statusUser = userManager.CreateAsync(users[0], "12qw!Q").GetAwaiter().GetResult();
                statusUser = userManager.CreateAsync(users[1], "12qw!Q").GetAwaiter().GetResult();
                statusUser = userManager.CreateAsync(users[2], "12qw!Q").GetAwaiter().GetResult();
                statusUser = userManager.CreateAsync(users[3], "12qw!Q").GetAwaiter().GetResult();
                statusUser = userManager.CreateAsync(users[4], "12qw!Q").GetAwaiter().GetResult();

                Shop[] shops = new Shop[] {
                    new Shop {Name = "Shop1"},
                    new Shop {Name = "Shop2"},
                    new Shop {Name = "Shop3"},
                };

                dataContext.Shops.AddRange(shops);

                var statusAddToRole = userManager.AddToRoleAsync(users[0], "PlatformAdministrator").GetAwaiter().GetResult();

                var usersDb = await dataContext.Set<User>()
                .ToListAsync();

                var rolesDb = await dataContext.Set<Role>()
                .ToListAsync();

                UserRoleShop[] userRoleShop = new UserRoleShop[] {
                    //new UserRoleShop {UserId = usersDb[0].Id, RoleId = rolesDb[2].Id},
                    new UserRoleShop {UserId = usersDb[1].Id, RoleId = rolesDb[0].Id,Shop = shops[0]},
                    new UserRoleShop {UserId = usersDb[1].Id, RoleId = rolesDb[0].Id,Shop = shops[1]},
                    new UserRoleShop {UserId = usersDb[2].Id, RoleId = rolesDb[0].Id,Shop = shops[2]},
                    new UserRoleShop {UserId = usersDb[2].Id, RoleId = rolesDb[1].Id,Shop = shops[0]},
                    new UserRoleShop {UserId = usersDb[3].Id, RoleId = rolesDb[1].Id,Shop = shops[1]},
                    new UserRoleShop {UserId = usersDb[4].Id, RoleId = rolesDb[1].Id,Shop = shops[2]},
                };

                dataContext.UserRoleShops.AddRange(userRoleShop);

                Product[] products = new Product[] {
                     new Product{ Name = "Child", Photo = ""},
                     new Product{ Name = "Sock", Photo = ""},
                     new Product{ Name = "Potato", Photo = ""},
                     new Product{ Name = "Cement", Photo = ""},
                };

                dataContext.Products.AddRange(products);

                Price[] prices = new Price[] {
                     new Price{ NetPrice = 1200, Shop = shops[0], Product = products[0], NumberProduct = 2},
                     new Price{ NetPrice = 1400, Shop = shops[1], Product = products[0], NumberProduct = 2},
                     new Price{ NetPrice = 1600, Shop = shops[2], Product = products[0], NumberProduct = 2},
                     new Price{ NetPrice = 400, Shop = shops[0], Product = products[1], NumberProduct = 7},
                     new Price{ NetPrice = 500, Shop = shops[1], Product = products[2], NumberProduct = 3},
                     new Price{ NetPrice = 600, Shop = shops[2], Product = products[3], NumberProduct = 5},
                };

                dataContext.SaveChanges();

                //statusAddToRole = userManager.AddToRoleAsync(users[1], "Owner").GetAwaiter().GetResult();
                //statusAddToRole = userManager.AddToRoleAsync(users[2], "Owner").GetAwaiter().GetResult();
                //statusAddToRole = userManager.AddToRoleAsync(users[2], "StoreAdministrator").GetAwaiter().GetResult();
                //statusAddToRole = userManager.AddToRoleAsync(users[3], "StoreAdministrator").GetAwaiter().GetResult();
                //statusAddToRole = userManager.AddToRoleAsync(users[4], "StoreAdministrator").GetAwaiter().GetResult();

                //UserShop[] userShops = new UserShop[] {
                //    new UserShop {Shop = shops[0], User = users[1]},
                //    new UserShop {Shop = shops[1], User = users[1]},
                //    new UserShop {Shop = shops[2], User = users[2]},
                //};

                //dataContext.UserShops.AddRange(userShops);

                //var userRoles = await dataContext.Set<UserRole>()
                //.ToListAsync();
            }
        }
    }
}