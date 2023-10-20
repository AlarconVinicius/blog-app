namespace Blog.Data.EntityConfig;

using Blog.Models.Recipes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RecipeBlogConfiguration : IEntityTypeConfiguration<RecipeBlog>
{
    public void Configure(EntityTypeBuilder<RecipeBlog> builder)
    {
        builder.ToTable("recipe_blogs");

        builder.HasKey(e => e.Id);

        builder.Property(c => c.Id)
            .HasColumnName("id");

        builder.Property(rb => rb.Title)
            .HasColumnName("title")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(rb => rb.Content)
            .HasColumnName("content")
            .HasColumnType("text")
            .IsRequired();

        builder.Property(rb => rb.CategoryId)
            .HasColumnName("category_id")
            .IsRequired();

        builder.Property(rb => rb.URL)
            .HasColumnName("url")
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(rb => rb.Difficulty)
            .HasColumnName("difficulty")
            .IsRequired();

        builder.Property(rb => rb.PreparationTime)
            .HasColumnName("preparation_time")
            .IsRequired();

        builder.Property(rb => rb.Servings)
            .HasColumnName("servings")
            .IsRequired()
            .HasDefaultValue(0)
            .IsConcurrencyToken();

        builder.Property(rb => rb.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(rb => rb.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        builder.HasIndex(rb => rb.Title).IsUnique();
        builder.HasIndex(rb => rb.URL).IsUnique();

        builder.HasOne(rb => rb.Category)
               .WithMany(c => c.Blogs)
               .HasForeignKey(rb => rb.CategoryId);

    }
}
