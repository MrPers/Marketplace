using Marketplace.DB;
using Marketplace.Web.Mappings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Marketplace.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            //services.AddScoped(typeof(IGroupRepository), typeof(GroupRepository));
            //services.AddScoped(typeof(ILetterRepository), typeof(LetterRepository));
            //services.AddScoped(typeof(ILetterStatusRepository), typeof(LetterStatusRepository));

            //services.AddAutoMapper(typeof(UserMapper));

            //services.AddScoped<ILetterService, LetterService>();
            //services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IGroupService, GroupService>();

            //services.AddScoped(typeof(ILetterLogics), typeof(LetterLogics));
            //services.AddScoped(typeof(ICacheLogics), typeof(CacheLogics));

            services.AddCors();
            services.AddControllers(); // используем контроллеры без представлений
            //services.AddMemoryCache(); //добавить кеширование в памяти


            string connection = Configuration.GetConnectionString("DataContext");
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connection));
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

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
