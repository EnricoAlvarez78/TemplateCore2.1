using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.EFContext
{
    public partial class SqlServerContext : DbContext
    {
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Role> Role { get; set; }
    }
}
