using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebWaterPaintStore.Core.Entities;

namespace WebWaterPaintStore.Data.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<Category>{
        public void Configure(EntityTypeBuilder<Category> builder)
        {

            builder.ToTable("Categories");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.UrlSlug)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Description)
                .HasMaxLength(200);

            builder.Property(a => a.Actived)
                .IsRequired()
                .HasDefaultValue(false);

        }

    }
}
