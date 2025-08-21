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

        public DbSet<SnmpDevice> SnmpDevices { get; set; }
        public DbSet<TcpDevice> TcpDevices { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Scripts> Scripts { get; set; }
        public DbSet<Alarms> Alarms { get; set; }
    }
}
