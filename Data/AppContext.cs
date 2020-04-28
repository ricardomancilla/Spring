using Domain.DbContext;
using Domain.Model;
using System.Data.Entity;

namespace Data
{
    public class AppContext : DbContext, IContext
    {
        public AppContext() : base("name=SpringDB")
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRol { get; set; }
    }
}
