using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Jobit.DAL.Database
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<JobitDbContext>
    {
        public JobitDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Directory.GetCurrentDirectory() + "/../Jobit.Web/appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<JobitDbContext>();
            var connectionString = configuration.GetSection("Data:AsmGpirDb:ConnectionString_Natalia").Value;
            builder.UseSqlServer(connectionString);
            return new JobitDbContext(builder.Options);
        }
    }
}