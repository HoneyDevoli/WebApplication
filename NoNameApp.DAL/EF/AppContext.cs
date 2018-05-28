using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoNameApp.Entities;
namespace NoNameApp.DAL.EF
{
    public class AppContext : DbContext
    {
        public DbSet<Episode>  Episodes { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Serial> Serials { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Order> Orders { get; set; }

        static AppContext()
        {
            Database.SetInitializer<AppContext>(new AppDbInitializer());
        }
        public AppContext(string connectionString)
            : base(connectionString)
        {
        }
        
    }
}
