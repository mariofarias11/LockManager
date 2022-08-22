using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LockManager.Infrastructure.DB.Context
{
    public class LockManagerDbContext : DbContext
    {
        private readonly string DBConnectionString;

        public LockManagerDbContext(IConfiguration configuration)
        {
            DBConnectionString = configuration.GetConnectionString("SqlConnectionString");
        }

        public LockManagerDbContext(DbContextOptions<LockManagerDbContext> options, IConfiguration configuration) : base(options)
        {
            DBConnectionString = configuration.GetConnectionString("SqlConnectionString");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DBConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LockManagerDbContext).Assembly);
            OnModelCreatingPartial(modelBuilder);
        }

        public void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
