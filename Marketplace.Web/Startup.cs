using IdentityServer4.EntityFramework.DbContexts;
using Marketplace.Contracts.Repository;
using Marketplace.Contracts.Services;
using Marketplace.DB;
using Marketplace.DB.Models;
using Marketplace.Repository;
using Marketplace.Service;
using Marketplace.Web.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Marketplace.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)  //при работе с базой добавмить процедуру, пока выгружаем все сразу
        {
            services.AddScoped(typeof(ICartRepository), typeof(CartRepository));
            services.AddScoped(typeof(IClaimRepository), typeof(ClaimRepository));
            services.AddScoped(typeof(ICommentProductRepository), typeof(CommentProductRepository));
            services.AddScoped(typeof(IPriceRepository), typeof(PriceRepository));
            services.AddScoped(typeof(IProductGroupRepository), typeof(ProductGroupRepository));
            services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
            services.AddScoped(typeof(IRoleRepository), typeof(RoleRepository));
            services.AddScoped(typeof(IShopRepository), typeof(ShopRepository));
            services.AddScoped(typeof(IStatusCartRepository), typeof(StatusCartRepository));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));

            services.AddAutoMapper(typeof(Mapper));

            //services.AddScoped<ICartService, CartService>();
            //services.AddScoped<IClaimService, ClaimService>();
            //services.AddScoped<ICommentProductService, CommentProductService>();
            services.AddScoped<IPriceService, PriceService>();
            //services.AddScoped<IProductGroupService, ProductGroupService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IShopService, ShopService>();
            //services.AddScoped<IStatusCartService, StatusCartService>();
            services.AddScoped<IUserService, UserService>();

            services.AddCors();
            services.AddControllers(); // используем контроллеры без представлений

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Test API",
                    Description = "ASP.NET Core Web API"
                });
            });

            services.AddDbContext<PersistedGrantDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DataContext"), b => b.MigrationsAssembly("Marketplace.DB"));
            });

            services.AddDbContext<ConfigurationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DataContext"), b => b.MigrationsAssembly("Marketplace.DB"));
            });

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DataContext"));
            })
            .AddIdentity<User, Role>(config =>
            {
                config.Password.RequireDigit = false;
                config.Password.RequireLowercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<DataContext>();

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddIdentityServer()
            .AddAspNetIdentity<User>()
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(Configuration.GetConnectionString(nameof(DataContext)),
                    sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(Configuration.GetConnectionString(nameof(DataContext)),
                    sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddProfileService<ProfileService>()
            .AddDeveloperSigningCredential();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1");
            });

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
