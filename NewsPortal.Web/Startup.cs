using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI.Services;
using NewsPortal.Dal;
using NewsPortal.Dal.Entities;
using NewsPortal.Dal.SeedInterfaces;
using NewsPortal.Dal.SeedServices;
using NewsPortal.Dal.Services;
using NewsPortal.Web.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using NewsPortal.Web.Hubs;
using NewsPortal.Web.ViewRender;

namespace NewsPortal.Web
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
            services.AddIdentity<User, IdentityRole<int>>()
            .AddEntityFrameworkStores<NewsPortalDbContext>()
            .AddDefaultTokenProviders();

            services.AddDbContext<NewsPortalDbContext>(o => o.UseSqlServer(Configuration.GetConnectionString(nameof(NewsPortalDbContext)))).AddTransient<ISeedService, SeedService>(); ;

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            });

            services.AddSignalR();
            services.AddScoped<NewsService>();
            services.AddScoped<CategoryService>();
            services.AddScoped<CommentService>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IViewRender, ViewRender.ViewRender>();

            services.AddScoped<IRoleSeedService, RoleSeedService>();
            services.AddScoped<IUserSeedService, UserSeedService>();

            services.AddMvc();
            services.AddControllersWithViews();
            services.AddRazorPages();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NewsHub>("/newshub");
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
