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
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "Controllers"))
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<McsAppDbContext>();
            var connectionString = configuration.GetConnectionString("Postgres");

            optionsBuilder.UseNpgsql(connectionString);

            return new McsAppDbContext(optionsBuilder.Options);
        }
    }
}
