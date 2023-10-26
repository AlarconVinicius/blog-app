using Business.Blog.Interfaces.Repositories;
using Business.Blog.Interfaces.Services;
using Business.Blog.Models.Recipe;
using Business.Configuration;

namespace Business.Blog.Services;

public class RecipePostService : MainService, IRecipePostService
{
    private readonly IRecipePostRepository _repository;
    public RecipePostService(IRecipePostRepository repository)
    {
        _repository = repository;
    }

    public async Task AddRecipe(RecipePost objeto)
    {
        try
        {
            await _repository.AddAsync(objeto);
            return;
        }
        catch (Exception ex)
        {
            AddProcessingError($"Erro ao adicionar receita: {ex.Message}");
            return;
        }
    }

    public async Task DeleteRecipe(Guid id)
    {
        try
        {
            await _repository.DeleteAsync(id);
            return;

        }
        catch (Exception ex)
        {
            AddProcessingError($"Falha ao deletar receita: {ex.Message}");
            return;
        }
    }

    public async Task<RecipePost> GetRecipeById(Guid id)
    {
        try
        {
            return await _repository.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            AddProcessingError($"Falha ao buscar receita: {ex.Message}");
            return null!;
        }
    }

    public async Task<List<RecipePost>> GetRecipes()
    {
        try
        {
            return await _repository.GetAllAsync();
        }
        catch (Exception ex)
        {
            AddProcessingError($"Falha ao buscar receita: {ex.Message}");
            return null!;
        }
    }

    public async Task UpdateRecipe(RecipePost objeto)
    {
        try
        {
            await _repository.UpdateAsync(objeto);
            return;

        }
        catch (Exception ex)
        {
            AddProcessingError($"Falha ao atualizar receita: {ex.Message}");
            return;
        }
    }
}
