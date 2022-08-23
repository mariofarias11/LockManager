using LockManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LockManager.Infrastructure.DB.Context
{
    public partial class LockManagerDbContext : DbContext
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

        public virtual DbSet<Door> Door { get; set; } = null!;
        public virtual DbSet<User> User { get; set; } = null!;
        public virtual DbSet<UserAuth> UserAuth { get; set; } = null!;

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

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
