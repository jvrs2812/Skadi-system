using backend.model;
using Microsoft.EntityFrameworkCore;

namespace backend.db
{
    public class SKADIDBContext : DbContext
    {
        public SKADIDBContext(DbContextOptions<SKADIDBContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enterprise>().OwnsOne(x => x.settings);
        }
        public DbSet<User>? User { get; set; }

        public DbSet<Enterprise>? Enterprise { get; set; }

        public DbSet<EnterpriseUser> EnterpriseUsers { get; set; }
    }
}