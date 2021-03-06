using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Marketplace.DB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IdentityModel.Tokens.Jwt;
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

            if (!bdContext.IdentityResources.Any())
            {
                foreach (var resource in IdentityServerConfiguration.GetIdentityResources())
                {
                    bdContext.IdentityResources.Add(resource.ToEntity());
                }
                bdContext.SaveChanges();
            }

            if (!bdContext.ApiScopes.Any())
            {
                foreach (var resource in IdentityServerConfiguration.GetApiScopes())
                {
                    bdContext.ApiScopes.Add(resource.ToEntity());
                }
                bdContext.SaveChanges();
            }

            if (!bdContext.Clients.Any())
            {
                foreach (var client in IdentityServerConfiguration.GetClients())
                {
                    bdContext.Clients.Add(client.ToEntity());
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
                    new User("AdmShop2AdmShop3"){Email = "Adm2@ukr.net"},
                    new User("User1"){Email = "User1@ukr.net"},
                    new User("User2"){Email = "User2@ukr.net"},
                };

                var statusUser = userManager.CreateAsync(users[0], "12qw!Q").GetAwaiter().GetResult();
                statusUser = userManager.CreateAsync(users[1], "12qw!Q").GetAwaiter().GetResult();
                statusUser = userManager.CreateAsync(users[2], "12qw!Q").GetAwaiter().GetResult();
                statusUser = userManager.CreateAsync(users[3], "12qw!Q").GetAwaiter().GetResult();
                statusUser = userManager.CreateAsync(users[4], "12qw!Q").GetAwaiter().GetResult();
                statusUser = userManager.CreateAsync(users[5], "12qw!Q").GetAwaiter().GetResult();

                Shop[] shops = new Shop[] {
                    new Shop {Name = "Shop1"},
                    new Shop {Name = "Shop2"},
                    new Shop {Name = "Shop3"},
                    new Shop {Name = "AllShop"},
                };

                dataContext.Shops.AddRange(shops);

                Claim[] claims = new Claim[] {
                    new Claim{ ClaimType = JwtRegisteredClaimNames.Jti, ClaimValue = "EditingAllUsers" },
                    new Claim{ ClaimType = JwtRegisteredClaimNames.Jti, ClaimValue = "EditingAllStores" },
                    new Claim{ ClaimType = JwtRegisteredClaimNames.Jti, ClaimValue = "EditingStore" },
                    new Claim{ ClaimType = JwtRegisteredClaimNames.Jti, ClaimValue = "DeletingStore" },
                };

                Role[] roles = new Role[] {
                    new Role("Owner"),
                    new Role("StoreAdministrator"),
                    new Role("PlatformAdministrator"),
                };

                roleManager.CreateAsync(roles[0]).GetAwaiter().GetResult();
                roleManager.CreateAsync(roles[1]).GetAwaiter().GetResult();
                roleManager.CreateAsync(roles[2]).GetAwaiter().GetResult();

                await roleManager.AddClaimAsync(roles[0], claims[2].ToClaim());
                await roleManager.AddClaimAsync(roles[0], claims[3].ToClaim());
                await roleManager.AddClaimAsync(roles[1], claims[2].ToClaim());
                await roleManager.AddClaimAsync(roles[2], claims[0].ToClaim());
                await roleManager.AddClaimAsync(roles[2], claims[1].ToClaim());

                var usersDb = await dataContext.Set<User>()
                .ToListAsync();

                var rolesDb = await dataContext.Set<Role>()
                .ToListAsync();

                UserRoleShop[] userRoleShop = new UserRoleShop[] {
                    new UserRoleShop {UserId = usersDb[1].Id, RoleId = rolesDb[0].Id,Shop = shops[0]},
                    new UserRoleShop {UserId = usersDb[1].Id, RoleId = rolesDb[0].Id,Shop = shops[1]},
                    new UserRoleShop {UserId = usersDb[2].Id, RoleId = rolesDb[0].Id,Shop = shops[2]},
                    new UserRoleShop {UserId = usersDb[2].Id, RoleId = rolesDb[1].Id,Shop = shops[0]},
                    new UserRoleShop {UserId = usersDb[3].Id, RoleId = rolesDb[1].Id,Shop = shops[1]},
                    new UserRoleShop {UserId = usersDb[3].Id, RoleId = rolesDb[1].Id,Shop = shops[2]},
                    new UserRoleShop {UserId = usersDb[0].Id, RoleId = rolesDb[2].Id,Shop = shops[3]},
                };

                dataContext.Set<UserRoleShop>().AddRange(userRoleShop);

                //на этот мамент в бд есть пользователи, роли, магазины и отношения между ними

                ProductGroup[] productGroups = new ProductGroup[] {
                     new ProductGroup{ Name = "edible"},
                     new ProductGroup{ Name = "not edible"},
                };

                dataContext.ProductGroups.AddRange(productGroups);

                Product[] products = new Product[] {
                     new Product{ Name = "Child", Photo = "../../../../../assets/images/child.jpg", ProductGroup = productGroups[0], Description = "#slavery - live"},
                     new Product{ Name = "Sock", Photo = "../../../../../assets/images/potato.jpg", ProductGroup = productGroups[1], Description = "Оплата при получении товара, Картой онлайн, Google Pay, -5% скидки от ПриватБанк и Mastercard от 500 грн, Безналичными для юридических лиц, Безналичными для физических лиц, PrivatPay, Apple Pay, Кредит, Оплата картой в отделении, Оплата частями"},
                     new Product{ Name = "Potato", Photo = "../../../../../assets/images/sock.jpg", ProductGroup = productGroups[0], Description = "#Belarus - free"},
                     new Product{ Name = "Cement", Photo = "../../../../../assets/images/tsement.jpg", ProductGroup = productGroups[1], Description = " искусственное неорганическое гидравлическое вяжущее вещество"},
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

                StatusUserChoice[] statusUserChoice = new StatusUserChoice[]{
                    new StatusUserChoice{ Name = "paid"},
                    new StatusUserChoice{ Name = "not paid"},
                    new StatusUserChoice{ Name = "delivered"},
                };

                dataContext.StatusUserChoices.AddRange(statusUserChoice);   //обрати внимание на коскадное удалиние при удалении юзера

                UserChoice[] userChoice = new UserChoice[]{
                     new UserChoice{User= users[4], NumberProduct = 1, Price = prices[5], StatusUserChoice = statusUserChoice[0] },
                     new UserChoice{User= users[5], NumberProduct = 2, Price = prices[1], StatusUserChoice = statusUserChoice[1] },
                     new UserChoice{User= users[4], NumberProduct = 1, Price = prices[2], StatusUserChoice = statusUserChoice[2] },
                     new UserChoice{User= users[5], NumberProduct = 2, Price = prices[3], StatusUserChoice = statusUserChoice[0] },
                     new UserChoice{User= users[3], NumberProduct = 1, Price = prices[4], StatusUserChoice = statusUserChoice[0] },
                     new UserChoice{User= users[2], NumberProduct = 2, Price = prices[3], StatusUserChoice = statusUserChoice[0] },
                     new UserChoice{User= users[3], NumberProduct = 1, Price = prices[0], StatusUserChoice = statusUserChoice[0] },
                     new UserChoice{User= users[2], NumberProduct = 2, Price = prices[5], StatusUserChoice = statusUserChoice[2] },
                     new UserChoice{User= users[5], NumberProduct = 1, Price = prices[2], StatusUserChoice = statusUserChoice[0] },
                     new UserChoice{User= users[3], NumberProduct = 2, Price = prices[1], StatusUserChoice = statusUserChoice[2] },
                     new UserChoice{User= users[2], NumberProduct = 1, Price = prices[0], StatusUserChoice = statusUserChoice[0] },
                };

                dataContext.UserChoices.AddRange(userChoice);

                CommentProduct[] commentProduct = new CommentProduct[]{
                     new CommentProduct{UserId = usersDb[5].Id, Product = products[0], DepartureDate = DateTime.Now, Text="CommentProduct good"},
                     new CommentProduct{UserId = usersDb[4].Id, Product = products[1], DepartureDate = DateTime.Now, Text="CommentProduct bad"},
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