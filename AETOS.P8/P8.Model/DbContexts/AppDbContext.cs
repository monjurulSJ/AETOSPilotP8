using Microsoft.EntityFrameworkCore;
using P8.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P8.Model.DbContexts
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
            
        }
        public virtual DbSet<Temperature> Temperatures { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<DeviceInfo> DeviceInfos { get; set; }
    }
}
