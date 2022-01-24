using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastruture.Data.Configurations
{
    public class ModuleConfiguration : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.ToTable("Module", "rol");

            builder.Property(e => e.ModuleId)
                .HasColumnName("moduleId")
                .ValueGeneratedNever();

            builder.Property(e => e.Description)
                .HasColumnName("description")
                .HasMaxLength(250);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(100);

            builder.Property(e => e.Route)
                .IsRequired()
                .HasColumnName("route")
                .HasMaxLength(150);
        }
    }
}
