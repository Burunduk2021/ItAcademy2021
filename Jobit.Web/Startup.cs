using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Jobit.Web.Models;
using Jobit.Web.Infrastructure.FileLogger;
using Jobit.Web.Infrastructure.UserModelValidator;
using ElmahCore;
using ElmahCore.Mvc;
using FluentValidation.AspNetCore;
using FormHelper;
using Jobit.DAL.Entities.Identity;
using Jobit.DAL.Database;

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
            services.AddDbContext<JobitDbContext>(options => options.UseSqlServer(Configuration["Data:AsmGpirDb:ConnectionString_Natalia"]));
            //Identity configuration
            services.AddIdentity<AppUser, IdentityRole>(opts=> {
                opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                                                      "0123456789- " + "àáâãäå¸æçèéêëìíîïğñòóôõö÷øùúûüışÿÀÁÂÃÄÅ¨ÆÇÈÉÊËÌÍÎÏĞÑÒÓÔÕÖ×ØÙÚÛÜİŞß";
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 6;
            }).AddEntityFrameworkStores<JobitDbContext>()
            .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(opts => opts.LoginPath = "/Account/Login");

            //ElmahCore
            services.AddElmah(opts =>
            {
                opts.Path = "/Admin/Logs";
                opts.OnPermissionCheck = context => context.User.Identity.IsAuthenticated;
            });
            services.AddElmah<XmlFileErrorLog>(options =>
            {
                options.LogPath = "~/logs";
            });
            //FluentValidator
            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserModelValidator>());
            //FormHelper
            services.AddControllersWithViews().AddFormHelper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
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

            //ElmahCore
            app.UseElmah();

            //FormHelper
            app.UseFormHelper();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //Create first admin account
            JobitDbContext.CreateAdminAccountAsync(app.ApplicationServices, Configuration).Wait();

            loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "log.txt"), (level) => 
                {
                if (level == LogLevel.Warning)
                { return true; }
                    return false;
            });
            var logger = loggerFactory.CreateLogger("FileLogger");



        }
    }
}
