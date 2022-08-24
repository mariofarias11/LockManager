using LockManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LockManager.Infrastructure.DB.Configuration
{
    public class DoorConfiguration: IEntityTypeConfiguration<Door>
    {
        public void Configure(EntityTypeBuilder<Door> entity)
        {
            entity.ToTable("Door");

            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.Open).HasColumnName("Open");
            entity.Property(e => e.MinimumRoleAuthorized).HasConversion<int>().HasColumnName("MinimumRoleAuthorized");
        }
    }
}
