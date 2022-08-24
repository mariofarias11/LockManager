using LockManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LockManager.Infrastructure.DB.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("User");

            entity.HasIndex(e => e.Username, "IX_Username");

            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.Active).HasColumnName("Active");
            entity.Property(e => e.Role).HasConversion<int>().HasColumnName("Role");
        }
    }
}
