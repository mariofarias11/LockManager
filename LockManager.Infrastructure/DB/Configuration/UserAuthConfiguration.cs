using LockManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LockManager.Infrastructure.DB.Configuration
{
    public class UserAuthConfiguration : IEntityTypeConfiguration<UserAuth>
    {
        public void Configure(EntityTypeBuilder<UserAuth> entity)
        {
            entity.ToTable("UserAuth");

            entity.HasIndex(e => e.Username, "IX_Username").IsUnique(true);

            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.Username).HasColumnName("Username");
            entity.Property(e => e.PasswordHash).HasColumnName("PasswordHash");
            entity.Property(e => e.PasswordSalt).HasColumnName("PasswordSalt");
            entity.Property(e => e.RefreshToken).HasColumnName("RefreshToken");
            entity.Property(e => e.TokenCreated).HasColumnName("TokenCreated");
            entity.Property(e => e.TokenExpires).HasColumnName("TokenExpires");
        }
    }
}
