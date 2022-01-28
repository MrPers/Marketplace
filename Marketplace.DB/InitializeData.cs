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
            await scopeServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.MigrateAsync();

            var bdContext = scopeServiceProvider.GetRequiredService<ConfigurationDbContext>();

            //var i = bdContext.Clients.ToList();

            if (!bdContext.Clients.Any())
            {
                foreach (var client in IdentityServerConfiguration.GetClients())
                {
                    bdContext.Clients.Add(client.ToEntity());
                }
                bdContext.SaveChanges();
            }

            if (!bdContext.IdentityResources.Any())
            {
                foreach (var resource in IdentityServerConfiguration.GetIdentityResources())
                {
                    bdContext.IdentityResources.Add(resource.ToEntity());
                }
                bdContext.SaveChanges();
            }

            if (!bdContext.ApiResources.Any())
            {
                foreach (var resource in IdentityServerConfiguration.GetApiResources())
                {
                    bdContext.ApiResources.Add(resource.ToEntity());
                }
                bdContext.SaveChanges();
            }

            await bdContext.Database.MigrateAsync();

            var dataContext = scopeServiceProvider.GetRequiredService<DataContext>();

            if (!dataContext.Users.Any() || !dataContext.Products.Any() || !dataContext.Shops.Any() || !dataContext.Roles.Any() || !dataContext.UserRoles.Any())
            {
                var userManager = scopeServiceProvider.GetService<UserManager<User>>();
                var roleManager = scopeServiceProvider.GetService<RoleManager<Role>>();

                User[] users = new User[] {
                    new User("AdministratorAll"){Email = "AdmAll@ukr.net", },
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

                //на этот мамент в бд есть пользователи, роли, магазины и отношения между ними

                ProductGroup[] productGroups = new ProductGroup[] {
                     new ProductGroup{ Name = "edible"},
                     new ProductGroup{ Name = "not edible"},
                };

                dataContext.ProductGroups.AddRange(productGroups);

                Product[] products = new Product[] {
                     new Product{ Name = "Child", Photo = "child.jpg", ProductGroup = productGroups[0]},
                     new Product{ Name = "Sock", Photo = "potato.jpg", ProductGroup = productGroups[1]},
                     new Product{ Name = "Potato", Photo = "sock.jpg", ProductGroup = productGroups[0]},
                     new Product{ Name = "Cement", Photo = "tsement.jpg", ProductGroup = productGroups[1] },
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

                dataContext.Prices.AddRange(prices);

                StatusCart[] statusCart = new StatusCart[]{
                     new StatusCart{ Name = "added"},
                     new StatusCart{ Name = "ordered"},
                     new StatusCart{ Name = "deleted"},
                };

                dataContext.StatusCarts.AddRange(statusCart);   //обрати внимание на коскадное удалиние при удалении юзера

                Cart[] cart = new Cart[]{
                     new Cart{User= users[1], NumberProduct = 1, Product = products[0] },
                     new Cart{User= users[1], NumberProduct = 1, Product = products[1] },
                     new Cart{User= users[1], NumberProduct = 1, Product = products[2] },
                     new Cart{User= users[2], NumberProduct = 2, Product = products[3] },
                     new Cart{User= users[2], NumberProduct = 1, Product = products[1] },
                     new Cart{User= users[0], NumberProduct = 2, Product = products[2] },
                     new Cart{User= users[0], NumberProduct = 1, Product = products[3] },
                };

                cart[0].StatusCarts.Add(statusCart[0]); //вот так надо объединять многие через многие
                cart[1].StatusCarts.Add(statusCart[1]);
                cart[2].StatusCarts.Add(statusCart[2]);
                cart[3].StatusCarts.Add(statusCart[0]);
                cart[4].StatusCarts.Add(statusCart[1]);
                cart[5].StatusCarts.Add(statusCart[0]);
                cart[6].StatusCarts.Add(statusCart[1]);

                dataContext.Carts.AddRange(cart);

                CommentProduct[] commentProduct = new CommentProduct[]{
                     new CommentProduct{UserId = usersDb[1].Id, Product = products[0], DepartureDate = DateTime.Now, Text="CommentProduct good"},
                     new CommentProduct{UserId = usersDb[0].Id, Product = products[1], DepartureDate = DateTime.Now, Text="CommentProduct bad"},
                     new CommentProduct{UserId = usersDb[3].Id, Product = products[1], DepartureDate = DateTime.Now, Text="CommentProduct liked"},
                     new CommentProduct{UserId = usersDb[2].Id, Product = products[2], DepartureDate = DateTime.Now, Text="CommentProduct bad"},
                     new CommentProduct{UserId = usersDb[2].Id, Product = products[3], DepartureDate = DateTime.Now, Text="CommentProduct liked"},
                };

                dataContext.CommentProducts.AddRange(commentProduct);

                dataContext.SaveChanges();

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