using Business.Interfaces.Repositories.Blog;
using Business.Interfaces.Services.Blog;
using Business.Models.Blog;

namespace Business.Services.Blog;

public class CategoryService : MainService, ICategoryService
{
    private readonly ICategoryRepository _repository;
    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task AddCategory(Category category)
    {
        try
        {
            if ((await _repository.GetCategoryByNameAndBlogId(category.Name, category.BlogId)) != null)
            {
                AddProcessingError("Erro ao adicionar categoria: Nome já existe.");
                return;
            };
            await _repository.AddAsync(category);
            return;
        }
        catch (Exception ex)
        {
            AddProcessingError($"Erro ao adicionar categoria: {ex.Message}");
            return;
        }
    }

    public async Task DeleteCategory(Guid id)
    {
        try
        {
            var categoryDb = await _repository.GetByIdAsync(id);
            if (categoryDb == null)
            {
                AddProcessingError("Falha ao deletar categoria: Categoria não encontrada.");
                return;
            };
            await _repository.DeleteAsync(id);
            return;

        }
        catch (Exception ex)
        {
            AddProcessingError($"Falha ao deletar categoria: {ex.Message}");
            return;
        }
    }

    public async Task<Category> GetCategoryById(Guid id)
    {
        try
        {
            var categoryDb = await _repository.GetByIdAsync(id);
            if (categoryDb == null)
            {
                AddProcessingError("Falha ao buscar categoria: Categoria não encontrada.");
                return null!;
            };
            return categoryDb;
        }
        catch (Exception ex)
        {
            AddProcessingError($"Falha ao buscar categoria: {ex.Message}");
            return null!;
        }
    }

    public async Task<List<Category>> GetAllCategories()
    {
        try
        {
            return await _repository.GetAllAsync();
        }
        catch (Exception ex)
        {
            AddProcessingError($"Falha ao buscar categoria: {ex.Message}");
            return null!;
        }
    }

    public async Task UpdateCategory(Guid id, Category category)
    {
        try
        {
            var categoryDb = await _repository.GetByIdAsync(id);
            if (categoryDb == null)
            {
                AddProcessingError("Falha ao atualizar categoria: Categoria não encontrada.");
                return;
            };
            categoryDb.Name = category.Name;
            await _repository.UpdateAsync(categoryDb);
            return;

        }
        catch (Exception ex)
        {
            AddProcessingError($"Falha ao atualizar categoria: {ex.Message}");
            return;
        }
    }
}
