using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration.Json;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace McsCore.AppDbContext
{
    internal class AppDbContextFactory : IDesignTimeDbContextFactory<McsAppDbContext>
    {
        public McsAppDbContext CreateDbContext(string[] args)
        {
            var basePath =  @"C:\Users\harun\source\repos\MonitoringControlSystemV3\src\Device\DeviceAPI\DeviceAPI";


            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionString = configuration.GetConnectionString("Postgres");
                
            var optionsBuilder = new DbContextOptionsBuilder<McsAppDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new McsAppDbContext(optionsBuilder.Options); 
        }
    }
}
