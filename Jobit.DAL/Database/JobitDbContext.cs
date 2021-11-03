using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Jobit.DAL.Entities.Aggregator;
using Jobit.DAL.Entities.Identity;

namespace Jobit.DAL.Database
{
    public class JobitDbContext : IdentityDbContext
    {
        //public static JobitDbContext GetJobitDbContext()
        //{
        //    var optionsBuilder = new DbContextOptionsBuilder<JobitDbContext>();
        //    string cstr = "Server=ARTUR-PC;Database=JobitDb;Trusted_Connection=True;MultipleActiveResultSets=true";
        //    optionsBuilder.UseSqlServer(cstr);
        //    return new JobitDbContext(optionsBuilder.Options);
        //}

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

        //Aggregator Entities
        public virtual DbSet<Vacancy> Vacancies { get; set; }
        public virtual DbSet<Employer> Employers { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<JobType> JobTypes{ get; set; }
        public virtual DbSet<JobSchedule> JobSchedules { get; set; }
        public virtual DbSet<JobUri> JobUris { get; set; }
        public virtual DbSet<JobSource> JobSources { get; set; }
        public virtual DbSet<Word> Words { get; set; }
        public virtual DbSet<KeyWord> KeyWords { get; set; }
        public virtual DbSet<CurrencyExchange> CurrencyExchanges { get; set; }

        /// <summary>
        /// Ввод предустановленных данных:
        ///     (1) четыре неизменяемые пользовательские роли;
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
            foreach (string roleName in AppUserRole.rolesArray)
            {
                userRole = await roleManager.FindByNameAsync(roleName);
                if (userRole == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //Создать первую учетную запись при первом запуске приложения
            UserManager<IdentityUser> userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string name = configuration["Data:AdminUser:Name"];
            string email = configuration["Data:AdminUser:Email"];
            string password = configuration["Data:AdminUser:Password"];
            if (await userManager.FindByEmailAsync(email) == null)
            {
                IdentityUser user = new IdentityUser
                {
                    Email = email,
                    UserName = name
                };

                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, AppUserRole.admin);
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

            modelBuilder.Entity<JobSource>().HasData(
                    JobSource.jobSourceesInitDb
            );

            modelBuilder.Entity<JobSchedule>().HasData(
                    JobSchedule.jobSchedulesInitDb
            );

            modelBuilder.Entity<JobType>().HasData(
                    JobType.jobTypesInitDb
            );

            modelBuilder.Entity<CurrencyExchange>().HasData(
                    CurrencyExchange.currencyExchangesInitDb
            );
        }
    }
}
