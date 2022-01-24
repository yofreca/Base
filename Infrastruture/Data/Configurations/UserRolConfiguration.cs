using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastruture.Data.Configurations
{
    public class UserRolConfiguration : IEntityTypeConfiguration<UserRol>
    {
        public void Configure(EntityTypeBuilder<UserRol> builder)
        {
            builder.HasKey(e => new { e.RolId, e.UserId });

            builder.ToTable("UserRol", "rol");

            builder.Property(e => e.RolId).HasColumnName("rolId");

            builder.Property(e => e.UserId).HasColumnName("userId");

            builder.HasOne(d => d.Rol)
                .WithMany(p => p.UserRol)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRol_Rol");

            builder.HasOne(d => d.User)
                .WithMany(p => p.UserRol)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRol_User");
        }
    }
}
