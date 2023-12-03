using Business.Models.Blog.Recipe;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings.Blog;
public class UserFavoriteRecipeConfiguration : IEntityTypeConfiguration<UserFavoriteRecipe>
{
    public void Configure(EntityTypeBuilder<UserFavoriteRecipe> builder)
    {
        builder.ToTable("user_favorite_recipes");

        builder.HasKey(e => e.Id);

        builder.Property(rb => rb.RecipeId)
               .HasColumnName("recipe_id")
               .IsRequired();

        builder.Property(rb => rb.UserId)
               .HasColumnName("user_id")
               .IsRequired();

        builder.HasOne(ub => ub.User)
               .WithMany(u => u.FavoriteRecipes)
               .HasForeignKey(ub => ub.UserId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(ub => ub.Recipe)
               .WithMany(b => b.UserFavoriteRecipes)
               .HasForeignKey(ub => ub.RecipeId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}
