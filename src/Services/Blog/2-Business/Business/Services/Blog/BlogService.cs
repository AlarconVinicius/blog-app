using Business.Interfaces.Repositories.Blog;
using Business.Interfaces.Services.Blog;
using Business.Models.Blog;

namespace Business.Services.Blog;

public class BlogService : MainService, IBlogService
{
    private readonly IBlogRepository _repository;
    public BlogService(IBlogRepository repository)
    {
        _repository = repository;
    }

    public async Task AddBlog(BlogEntity blog)
    {
        try
        {
            if ((await _repository.GetBlogByName(blog.Name)) != null)
            {
                AddProcessingError("Erro ao adicionar blog: Nome já existe.");
                return;
            };
            blog.NormalizeName();
            await _repository.AddAsync(blog);
            return;
        }
        catch (Exception ex)
        {
            AddProcessingError($"Erro ao adicionar blog: {ex.Message}");
            return;
        }
    }

    public async Task DeleteBlog(Guid id)
    {
        try
        {
            var blogDb = await _repository.GetByIdAsync(id);
            if (blogDb == null)
            {
                AddProcessingError("Falha ao deletar blog: Blog não encontrado.");
                return;
            };
            await _repository.DeleteAsync(id);
            return;

        }
        catch (Exception ex)
        {
            AddProcessingError($"Falha ao deletar blog: {ex.Message}");
            return;
        }
    }

    public async Task<BlogEntity> GetBlogById(Guid id)
    {
        try
        {
            var blogDb = await _repository.GetByIdAsync(id);
            if (blogDb == null)
            {
                AddProcessingError("Falha ao buscar blog: Blog não encontrado.");
                return null!;
            };
            return blogDb;
        }
        catch (Exception ex)
        {
            AddProcessingError($"Falha ao buscar blog: {ex.Message}");
            return null!;
        }
    }

    public async Task<List<BlogEntity>> GetAllBlogs()
    {
        try
        {
            return await _repository.GetAllAsync();
        }
        catch (Exception ex)
        {
            AddProcessingError($"Falha ao buscar blog: {ex.Message}");
            return null!;
        }
    }

    public async Task UpdateBlog(Guid id, BlogEntity blog)
    {
        try
        {
            var blogDb = await _repository.GetByIdAsync(id);
            if (blogDb == null)
            {
                AddProcessingError("Falha ao atualizar blog: Blog não encontrado.");
                return;
            };
            blogDb.Name = blog.Name;
            blogDb.NormalizeName();
            await _repository.UpdateAsync(blogDb);
            return;

        }
        catch (Exception ex)
        {
            AddProcessingError($"Falha ao atualizar blog: {ex.Message}");
            return;
        }
    }
}
