using Business.Models.Auth;
using Business.Models.Blog;
using Business.Models.Blog.Recipe;
using Data.Mappings.Blog;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Configuration;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<RecipePost> Recipes { get; set; }
    public DbSet<UserFavoriteRecipe> UserFavoriteRecipes { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers");
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new RecipePostConfiguration());
        modelBuilder.ApplyConfiguration(new UserFavoriteRecipeConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}