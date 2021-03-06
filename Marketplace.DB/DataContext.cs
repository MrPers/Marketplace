using Marketplace.DB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.DB
{
    public class DataContext : IdentityDbContext<
    User, // TUser
    Role, // TRole
    Guid, // TKey
    IdentityUserClaim<Guid>, // TUserClaim
    UserRoleShop, // TUserRole,
    IdentityUserLogin<Guid>, // TUserLogin
    Claim, // TRoleClaim
    IdentityUserToken<Guid> // TUserToken
    >
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<UserChoice> UserChoices { get; set; }
        //public DbSet<Claim> Claims { get; set; }
        public DbSet<CommentProduct> CommentProducts { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<StatusUserChoice> StatusUserChoices { get; set; }
        //public DbSet<UserRoleShop> UserRoleShops { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRoleShop>()
                .HasKey(ur => new { ur.UserId, ur.RoleId, ur.ShopId });

            modelBuilder.Entity<UserRoleShop>()
                .HasOne(sc => sc.Shop)
                .WithMany(c => c.UserRoles)
                .HasForeignKey(sc => sc.ShopId)
                .HasPrincipalKey(sc => sc.Id);

            modelBuilder.Entity<UserRoleShop>()
                .HasOne(sc => sc.User)
                .WithMany(c => c.UserRoleShops)
                .HasForeignKey(sc => sc.UserId)
                .HasPrincipalKey(sc => sc.Id);

            modelBuilder.Entity<UserRoleShop>()
                .HasOne(sc => sc.Role)
                .WithMany(c => c.UserRoleShops)
                .HasForeignKey(sc => sc.RoleId)
                .HasPrincipalKey(sc => sc.Id);

            modelBuilder.Entity<UserChoice>()
                .HasKey(ur => new { ur.Id, ur.PriceId, ur.UserId });
            //.HasKey(ur => new {ur.ProductId, ur.UserId });

            modelBuilder.Entity<UserChoice>()
                .HasOne(sc => sc.User)
                .WithMany(s => s.UserChoices)
                .HasForeignKey(sc => sc.UserId)
                .HasPrincipalKey(sc => sc.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserChoice>()
                .HasOne(sc => sc.Price)
                .WithMany(c => c.UserChoices)
                .HasForeignKey(sc => sc.PriceId)
                .HasPrincipalKey(sc => sc.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CommentProduct>()
                .HasKey(ur => new { ur.Id, ur.ProductId, ur.UserId });
                //.HasKey(ur => new {ur.ProductId, ur.UserId });

            modelBuilder.Entity<CommentProduct>()
                .HasOne(sc => sc.User)
                .WithMany(s => s.CommentProducts)
                .HasForeignKey(sc => sc.UserId)
                .HasPrincipalKey(sc => sc.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CommentProduct>()
                .HasOne(sc => sc.Product)
                .WithMany(c => c.CommentProducts)
                .HasForeignKey(sc => sc.ProductId)
                .HasPrincipalKey(sc => sc.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Price>()
                .HasKey(ur => new { ur.Id, ur.ProductId, ur.ShopId });
                //.HasKey(ur => new {ur.ProductId, ur.ShopId });

            modelBuilder.Entity<Price>()
                .HasOne(sc => sc.Shop)
                .WithMany(s => s.Prices)
                .HasForeignKey(sc => sc.ShopId)
                .HasPrincipalKey(sc => sc.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Price>()
                .HasOne(sc => sc.Product)
                .WithMany(c => c.Prices)
                .HasForeignKey(sc => sc.ProductId)
                .HasPrincipalKey(sc => sc.Id)
                .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<LetterUser>()
            //    .Property(b => b.Status)
            //    .HasDefaultValue(false);

            //modelBuilder.Entity<User>()
            //    .Property(b => b.Surname)
            //    .HasDefaultValue("RT");

            //base.OnModelCreating(modelBuilder);
        }
    }
}