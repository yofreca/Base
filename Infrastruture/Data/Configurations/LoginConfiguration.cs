using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastruture.Data.Configurations
{
    public class LoginConfiguration : IEntityTypeConfiguration<Login>
    {
        public void Configure(EntityTypeBuilder<Login> builder)
        {
            builder.ToTable("Login", "Authorization");

            builder.Property(e => e.LoginId).HasColumnName("loginId");

            builder.Property(e => e.Password)
                .IsRequired()
                .HasColumnName("password")
                .HasMaxLength(100);

            builder.Property(e => e.UserId).HasColumnName("userId");

            builder.Property(e => e.UserName)
                .IsRequired()
                .HasColumnName("userName")
                .HasMaxLength(100);

            builder.HasOne(d => d.User)
                .WithMany(p => p.Login)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Login_UserDetail");
        }
    }
}
