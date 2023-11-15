using Business.Models.Blog.Dtos;
using Business.Models.Blog.Recipe;

namespace Business.Mappings.Blog;

public static class DifficultyMapping
{
    public static DifficultyViewDto ToDto(this Difficulty difficulty)
    {
        return new DifficultyViewDto((int)difficulty, difficulty.ToString());
    }
}