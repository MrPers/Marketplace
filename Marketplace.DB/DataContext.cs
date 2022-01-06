using Marketplace.DB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Marketplace.DB
{
    public class DataContext : IdentityDbContext<
    User, // TUser
    Role, // TRole
    Guid, // TKey
    IdentityUserClaim<Guid>, // TUserClaim
    UserRolePlatform, // TUserRole,
    IdentityUserLogin<Guid>, // TUserLogin
    IdentityRoleClaim<Guid>, // TRoleClaim
    IdentityUserToken<Guid> // TUserToken
    >
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<CommentProduct> CommentProducts { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<StatusCart> StatusCarts { get; set; }
        public DbSet<UserRoleShop> UserRoleShops { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRoleShop>()
                .HasKey(ur => new { ur.UserId, ur.RoleId, ur.ShopId });

            //modelBuilder.Entity<UserRole>()   //допустить значение null
            //    .Property(m => m.ShopId)
            //    .IsRequired(false);

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

            modelBuilder.Entity<Cart>()
                .HasOne(sc => sc.User)
                .WithMany(s => s.Carts)
                .HasForeignKey(sc => sc.UserId)
                .HasPrincipalKey(sc => sc.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Cart>()
                .HasOne(sc => sc.Product)
                .WithMany(c => c.Carts)
                .HasForeignKey(sc => sc.ProductId)
                .HasPrincipalKey(sc => sc.Id)
                .OnDelete(DeleteBehavior.Cascade);

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