using Business.Models.Blog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings.Blog;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");

        builder.HasKey(e => e.Id);

        builder.Property(c => c.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(50);

        builder.HasMany(c => c.RecipePosts)
               .WithOne(rb => rb.Category)
               .HasForeignKey(rb => rb.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
