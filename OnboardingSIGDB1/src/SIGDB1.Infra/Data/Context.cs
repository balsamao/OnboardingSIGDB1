using Microsoft.EntityFrameworkCore;
using SIGDB1.Domain.Entities;

namespace SIGDB1.Infra.Data
{
    public class Context : DbContext
    {
        public DbSet<Company> Companies { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
        }
    }
}
