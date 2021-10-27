using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Jobit.Web.Models;

namespace Jobit.Web.Database
{
    class JobitDbContext : IdentityDbContext<AppUser>
    {
        public JobitDbContext(DbContextOptions<JobitDbContext> options) : base(options)
        {
#if ZEROMODE
            //(1)Ensures that the database for the context does not exist.
            //If it does not exist, no action is taken.
            //If it does exist then the database is deleted.
            Database.EnsureDeleted();

            //(2) Ensures that the database for the context exists.
            //If it exists, no action is taken.
            //If it does not exist then the database and all its schema are created.
            Database.EnsureCreated();
#endif
        }

        /// <summary>
        /// Ввод предустановленных данных:
        ///     (1) 2 неизменяемые пользовательские роли;
        ///     (2) первой учетной записи "администратор". Он уже сможет вручную создать новые записи. Иначе в систему невозможно будет зайти.
        /// </summary>
        /// <param name="serviceProvider">Обеспечивает разовое подключение службы управления пользователями и ролями для создания УЗ</param>
        /// <param name="configuration">Обеспечивает чтение из appsettings.json</param>
        /// <returns>void</returns>
        public static async Task CreateAdminAccountAsync(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            //Создать две предустановленные пользовательские роли
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            IdentityRole userRole;
            foreach (string roleName in DAL.Entities.Identity.AppUserRole.rolesArray)
            {
                userRole = await roleManager.FindByNameAsync(roleName);
                if (userRole == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //Создать первую учетную запись при первом запуске приложения
            UserManager<AppUser> userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            string name = configuration["Data:AdminUser:Name"];
            string email = configuration["Data:AdminUser:Email"];
            string password = configuration["Data:AdminUser:Password"];
            if (await userManager.FindByEmailAsync(email) == null)
            {
                AppUser user = new AppUser
                {
                    Email = email,
                    UserName = name
                };

                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, DAL.Entities.Identity.AppUserRole.admin);
                }
            }
        }

        /// <summary>
        /// Инициализация базы данных начальными значениями при создании модели.
        /// </summary>
        /// <param name="modelBuilder">Класс для создания модели данных</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
