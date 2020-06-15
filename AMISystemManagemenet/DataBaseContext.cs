using Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMISystemManagemenet
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext() : base()
        {

        }

        public DbSet<DevClass> Models { get; set; }
        public DbSet<Agregator> Controllers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>().ToTable("dbo.Devices");
        }
    }
}
