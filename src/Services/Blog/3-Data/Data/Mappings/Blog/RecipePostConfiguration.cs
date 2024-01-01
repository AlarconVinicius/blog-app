using Business.Models.Blog.Recipe;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings.Blog;

public class RecipePostConfiguration : IEntityTypeConfiguration<RecipePost>
{
    public void Configure(EntityTypeBuilder<RecipePost> builder)
    {
        builder.ToTable("recipes");

        builder.HasKey(e => e.Id);

        builder.Property(rb => rb.Title)
            .HasColumnName("title")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(rb => rb.PreparationSteps)
            .HasColumnName("preparation_steps")
            .HasColumnType("text")
            .IsRequired();

        builder.Property(rb => rb.Ingredients)
            .HasColumnName("ingredients")
            .HasColumnType("text")
            .IsRequired();

        builder.Property(rb => rb.UserId)
            .HasColumnName("user_id")
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
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(rb => rb.Servings)
            .HasColumnName("servings")
            .IsRequired()
            .HasDefaultValue(0)
            .IsConcurrencyToken();

        builder.HasIndex(rb => rb.Title).IsUnique();
        builder.HasIndex(rb => rb.URL).IsUnique();

        builder.HasOne(rb => rb.Category)
               .WithMany(c => c.RecipePosts)
               .HasForeignKey(rb => rb.CategoryId);
        //.OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(rb => rb.User)
               .WithMany(c => c.RecipePosts)
               .HasForeignKey(rb => rb.UserId);


    }
}
