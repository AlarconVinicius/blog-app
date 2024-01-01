using Business.Models.Blog;
using Business.Models.Blog.Dtos;

namespace Business.Mappings.Blog;

public static class CategoryMapping
{
    public static CategoryViewDto ToDto(this Category category)
    {
        return new CategoryViewDto(category.Id, category.Name);
    }

    public static Category ToDomain(this CategoryAddDto category)
    {
        return new Category(category.Name);
    }
}
