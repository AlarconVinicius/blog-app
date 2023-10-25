using Business.Blog.Models;
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

        builder.Property(rb => rb.BlogId)
            .HasColumnName("blog_id")
            .IsRequired();

        builder.HasOne(c => c.Blog)
               .WithMany(rb => rb.Categories)
               .HasForeignKey(rb => rb.BlogId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.RecipePosts)
               .WithOne(rb => rb.Category)
               .HasForeignKey(rb => rb.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
