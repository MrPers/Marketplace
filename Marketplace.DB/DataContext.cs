using Marketplace.DB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.DB
{
    public class DataContext : IdentityDbContext<
    User, // TUser
    Role, // TRole
    long, // TKey
    IdentityUserClaim<long>, // TUserClaim
    UserRole, // TUserRole,
    IdentityUserLogin<long>, // TUserLogin
    IdentityRoleClaim<long>, // TRoleClaim
    IdentityUserToken<long> // TUserToken
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
        public DbSet<UserShop> UserShops { get; set; }
        public DbSet<RoleShop> RoleShops { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserRole>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            //modelBuilder.Entity<UserRole>()
            //    .HasKey(hk => new { hk.UserId, hk.RoleId, hk.Id });
            //    .HasKey(hk => new { hk.Id });

            modelBuilder.Entity<RoleShop>()
                .HasOne(sc => sc.UserRole)
                .WithMany(c => c.RoleShops)
                .HasForeignKey(sc => sc.UserRoleId)
                .HasPrincipalKey(sc => sc.Id)
                //.OnDelete(DeleteBehavior.SetNull);
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RoleShop>()
                .HasOne(sc => sc.UserShop)
                .WithMany(c => c.RoleShops)
                .HasForeignKey(sc => sc.UserShopId)
                .HasPrincipalKey(sc => sc.Id)
                //.OnDelete(DeleteBehavior.SetNull);
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserShop>()
                .HasOne(sc => sc.Shop)
                .WithMany(c => c.UserShops)
                .HasForeignKey(sc => sc.ShopId)
                .HasPrincipalKey(sc => sc.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserShop>()
                .HasOne(sc => sc.User)
                .WithMany(c => c.UserShops)
                .HasForeignKey(sc => sc.UserId)
                .HasPrincipalKey(sc => sc.Id)
                .OnDelete(DeleteBehavior.Cascade);

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

            base.OnModelCreating(modelBuilder);
        }
    }
}