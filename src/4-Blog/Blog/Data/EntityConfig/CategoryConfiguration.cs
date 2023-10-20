using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.EntityConfig;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");

        builder.HasKey(e => e.Id);

        builder.Property(c => c.Id)
            .HasColumnName("id");

        builder.Property(c => c.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(rb => rb.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(rb => rb.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        builder.HasMany(c => c.Blogs)
               .WithOne(rb => rb.Category)
               .HasForeignKey(rb => rb.CategoryId);
    }
}
