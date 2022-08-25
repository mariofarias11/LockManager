using LockManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LockManager.Infrastructure.DB.Context
{
    public partial class LockManagerDbContext : DbContext
    {
        private readonly string _dbConnectionString;

        public LockManagerDbContext(IConfiguration configuration)
        {
            _dbConnectionString = configuration.GetConnectionString("SqlConnectionString");
        }

        public LockManagerDbContext(DbContextOptions<LockManagerDbContext> options, IConfiguration configuration) : base(options)
        {
            _dbConnectionString = configuration.GetConnectionString("SqlConnectionString");
        }

        public virtual DbSet<Door> Door { get; set; } = null!;
        public virtual DbSet<User> User { get; set; } = null!;
        public virtual DbSet<UserAuth> UserAuth { get; set; } = null!;
        public virtual DbSet<DoorHistory> DoorHistory { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_dbConnectionString);
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
