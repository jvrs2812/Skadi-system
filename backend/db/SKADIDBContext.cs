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
        public DbSet<User> User { get; set; }
    }
}