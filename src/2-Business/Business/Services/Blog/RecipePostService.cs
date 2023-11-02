using Business.Helpers.Auth;
using Business.Interfaces.Repositories.Blog;
using Business.Interfaces.Services.Blog;
using Business.Mappings.Blog;
using Business.Models.Blog.Dtos;
using Business.Models.Blog.Recipe;
using Microsoft.AspNetCore.Http;

namespace Business.Services.Blog;

public class RecipePostService : MainService, IRecipePostService
{
    private readonly Guid blogId = Guid.Parse("2a2ff613-6f3b-4dd8-9fd6-a2f824b67b62");
    private readonly IRecipePostRepository _repository;
    private readonly IHttpContextAccessor _httpAccessor;
    public RecipePostService(IRecipePostRepository repository, IHttpContextAccessor httpAccessor)
    {
        _repository = repository;
        _httpAccessor = httpAccessor;
    }

    public async Task AddRecipe(RecipePostAddDto recipe)
    {
        try
        {
            var recipeDb = await _repository.GetRecipeByTitle(recipe.Title);
            if(recipeDb != null)
            {
                AddProcessingError($"Erro ao adicionar receita: Título já existe");
                return;
            }
            var recipeMapped = recipe.ToDomain();
            recipeMapped.BlogId = blogId;
            recipeMapped.UserId = AuthHelper.GetUserId(_httpAccessor).ToString();
            recipeMapped.GenerateURL();
            await _repository.AddAsync(recipeMapped);
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

    public async Task<RecipePostViewDto> GetRecipeById(Guid id)
    {
        try
        {
            var recipeDb = await _repository.GetRecipeById(id);
            return recipeDb.ToDto();
        }
        catch (Exception ex)
        {
            AddProcessingError($"Falha ao buscar receita: {ex.Message}");
            return null!;
        }
    }

    public async Task<IEnumerable<RecipePostViewDto>> GetRecipes()
    {
        try
        {
            var recipeDb = await _repository.GetAllRecipes();
            return recipeDb.Select(x => x.ToDto());
        }
        catch (Exception ex)
        {
            AddProcessingError($"Falha ao buscar receita: {ex.Message}");
            return null!;
        }
    }

    public async Task UpdateRecipe(RecipePost recipe)
    {
        try
        {
            recipe.GenerateURL();
            await _repository.UpdateAsync(recipe);
            return;

        }
        catch (Exception ex)
        {
            AddProcessingError($"Falha ao atualizar receita: {ex.Message}");
            return;
        }
    }
}
