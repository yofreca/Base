using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastruture.Data.Configurations
{
    public class RolPermitsConfiguration : IEntityTypeConfiguration<RolPermits>
    {
        public void Configure(EntityTypeBuilder<RolPermits> builder)
        {
            builder.HasKey(e => new { e.ModuleId, e.RolId });

            builder.ToTable("RolPermits", "rol");

            builder.Property(e => e.ModuleId).HasColumnName("moduleId");

            builder.Property(e => e.RolId).HasColumnName("rolId");

            builder.Property(e => e.PermitId).HasColumnName("permitId");

            builder.HasOne(d => d.Module)
                .WithMany(p => p.RolPermits)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolPermits_Module");

            builder.HasOne(d => d.Rol)
                .WithMany(p => p.RolPermits)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolPermits_Rol");
        }
    }
}
