using Business.Interfaces.Repositories.Blog;
using Business.Models.Blog.Recipe;
using Data.Configuration;

namespace Data.Blog.Repositories;

public class RecipePostRepository : BaseRepository<RecipePost>, IRecipePostRepository
{

    public RecipePostRepository(ApplicationDbContext context) : base(context)
    {
    }
}
