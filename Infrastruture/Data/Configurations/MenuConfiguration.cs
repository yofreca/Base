using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastruture.Data.Configurations
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menu", "rol");

            builder.Property(e => e.MenuId).HasColumnName("menuId");

            builder.Property(e => e.Icon)
                .HasColumnName("icon")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.IdParentMenu).HasColumnName("idParentMenu");

            builder.Property(e => e.LevelMenu).HasColumnName("levelMenu");

            builder.Property(e => e.MenuName)
                .HasColumnName("menuName")
                .HasMaxLength(100);

            builder.Property(e => e.ModuleId).HasColumnName("moduleId");

            builder.Property(e => e.Order).HasColumnName("order");

            builder.HasOne(d => d.Module)
                .WithMany(p => p.Menu)
                .HasForeignKey(d => d.ModuleId)
                .HasConstraintName("FK_Menu_Module");
        }
    }
}
