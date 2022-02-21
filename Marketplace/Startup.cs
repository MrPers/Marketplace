using Marketplace.Contracts.Repository;
using Marketplace.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Reflection;
using Marketplace.Contracts.Services;
using Marketplace.Service;
using Marketplace.DB;
using Marketplace.DB.Models;
using Marketplace.Infrastructure;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Marketplace
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services
            .AddDbContext<PersistedGrantDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DataContext"), b => b.MigrationsAssembly("Marketplace.DB"));
            })
            .AddDbContext<ConfigurationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DataContext"), b => b.MigrationsAssembly("Marketplace.DB"));
            })
            .AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DataContext"), b => b.MigrationsAssembly("Marketplace.DB"));
            })
            .AddIdentity<User, Role>(config =>
            {
                config.Password.RequireDigit = false;
                config.Password.RequireLowercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddAspNetIdentity<User>()
                //.AddInMemoryApiScopes(IdentityServerConfiguration.GetApiScopes())
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(Configuration.GetConnectionString(nameof(DataContext)),//
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(Configuration.GetConnectionString(nameof(DataContext)),
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddProfileService<ProfileService>();

            services.AddSingleton<ICorsPolicyService>((container) =>        //CORS от IS4
            {
                var logger = container.GetRequiredService<ILogger<DefaultCorsPolicyService>>();
                return new DefaultCorsPolicyService(logger)
                {
                    AllowedOrigins = { "http://localhost:4200" }
                };
            });

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = Configuration["ApplicationUrl"].TrimEnd('/');
                    options.SupportedTokens = SupportedTokens.Jwt;
                    options.RequireHttpsMetadata = false; // Note: Set to true in production
                    options.ApiName = IdentityServerConfiguration.ApiName;
                });

            services.AddAuthorization();

            services.AddCors();     // Add cors

            services.AddControllersWithViews(); // для работы контроллеров MVC

            //services.AddSpaStaticFiles(configuration =>
            //{
            //    configuration.RootPath = "ClientApp/dist";
            //});

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = IdentityServerConfiguration.ApiName, Version = "v1" });
                c.OperationFilter<AuthorizeCheckOperationFilter>();
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri("/connect/token", UriKind.Relative),
                            Scopes = new Dictionary<string, string>()
                            {
                                { IdentityServerConfiguration.ApiName, IdentityServerConfiguration.ApiName }    // ApiFriendlyName
                            }
                        }
                    }
                });

            });

            services.AddAutoMapper(typeof(Mapper));

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

            //services.AddScoped<ICartService, CartService>();
            //services.AddScoped<IClaimService, ClaimService>();
            services.AddScoped<ICommentProductService, CommentProductService>();
            services.AddScoped<IPriceService, PriceService>();
            services.AddScoped<IProductGroupService, ProductGroupService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IShopService, ShopService>();
            //services.AddScoped<IStatusCartService, StatusCartService>();
            services.AddScoped<IUserService, UserService>();

            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.Events.OnRedirectToAccessDenied =
            //        options.Events.OnRedirectToLogin = context =>
            //        {
            //            if (context.Request.Method != "GET")
            //            {
            //                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //                return Task.FromResult<object>(null);
            //            }
            //            context.Response.Redirect(context.RedirectUri);
            //            return Task.FromResult<object>(null);
            //        };
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //if (!env.IsDevelopment())
            //{
            //    app.UseSpaStaticFiles();
            //}

            app.UseRouting();
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "Swagger UI - ExampleApi";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{IdentityServerConfiguration.ApiName} V1");
                c.OAuthClientId(IdentityServerConfiguration.SwaggerClientID);
                c.OAuthClientSecret("no_password"); //Оставить поле пустым не работает
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

        }
    }
}
