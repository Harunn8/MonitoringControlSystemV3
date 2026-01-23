using McsCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McsCore.AppDbContext
{
    public class McsAppDbContext : DbContext
    {
        public McsAppDbContext(DbContextOptions<McsAppDbContext> options) : base(options) { }

        public DbSet<BaseDeviceModel> Devices { get; set; }
        public DbSet<PagDevices> PagDevices { get; set; }
        public DbSet<Pags> Pags { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Scripts> Scripts { get; set; }
        public DbSet<Alarms> Alarms { get; set; }
        public DbSet<ParameterLogsAdd> ParameterLogs { get; set; }
        public DbSet<ParameterLogTs> ParameterLogsTs { get; set; }
    }
}
