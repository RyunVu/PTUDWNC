using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebWaterPaintStore.Core.Entities;

namespace WebWaterPaintStore.Data.Mappings
{
    public class AccountMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(256);

            builder.Property(p => p.Email)
                .IsRequired();

            builder.Property(p => p.Username)
                .IsRequired()
               .HasMaxLength(128);

            builder.Property(p => p.Password)
                .IsRequired()
               .HasMaxLength(512);

            builder.HasMany(p => p.Roles)
                .WithMany(p => p.Users)
               .UsingEntity(pt => pt.ToTable("UserInRoles"));
        }
    }

    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(128);
        }
    }
}
