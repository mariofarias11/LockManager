using LockManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LockManager.Infrastructure.DB.Configuration
{
    public class DoorHistoryConfiguration : IEntityTypeConfiguration<DoorHistory>
    {
        public void Configure(EntityTypeBuilder<DoorHistory> entity)
        {
            entity.ToTable("DoorHistory");

            entity.HasIndex(e => e.DoorId, "IX_Door").IsUnique(false);
            entity.HasIndex(e => e.UserId, "IX_User").IsUnique(false);

            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.DoorId).HasColumnName("DoorId");
            entity.Property(e => e.UserId).HasColumnName("UserId");
            entity.Property(e => e.EntryDateTime).HasColumnName("EntryDateTime");
            entity.Property(e => e.IsSuccessfulEntry).HasColumnName("IsSuccessfulEntry");

            entity.HasOne(d => d.Door)
                .WithMany(p => p.DoorHistoryList)
                .HasForeignKey(d => d.DoorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Door_Id");
        }
    }
}
