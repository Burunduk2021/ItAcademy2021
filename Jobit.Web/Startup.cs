using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Jobit.Web.Database;

namespace Jobit.Web
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
            //MVC infrastucture configuration
            services.AddControllersWithViews();
            
            //SQL Db and repositories settings
            services.AddDbContext<JobitDbContext>(options => options.UseSqlServer(Configuration["Data:AsmGpirDb:ConnectionString"]));
            
            //Identity configuration
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<JobitDbContext>()
                .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(opts => opts.LoginPath = "/Identity/Account/Login");
            
            //Dependency injections

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                //Processes requests to execute migrations operations. The middleware will listen for requests made to DefaultPath.
                app.UseMigrationsEndPoint();
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
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //Create two user-roles and first admin account
            JobitDbContext.CreateAdminAccountAsync(app.ApplicationServices, Configuration).Wait();
        }
    }
}
