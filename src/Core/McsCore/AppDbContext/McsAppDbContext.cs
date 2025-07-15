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
    }
}
