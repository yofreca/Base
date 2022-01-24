using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastruture.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User", "Authorization");

            builder.Property(e => e.UserId)
                .HasColumnName("userId");
                
            builder.Property(e => e.Charge)
                .IsRequired()
                .HasColumnName("charge")
                .HasMaxLength(100);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasColumnName("email")
                .HasMaxLength(100);

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasColumnName("firstName")
                .HasMaxLength(100);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasColumnName("lastName")
                .HasMaxLength(100);

            builder.Property(e => e.Phone)
                .IsRequired()
                .HasColumnName("phone")
                .HasMaxLength(100);
        }
    }
}
