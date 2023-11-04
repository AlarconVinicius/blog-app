using Business.Helpers.Auth;
using Business.Interfaces.Repositories.Blog;
using Business.Interfaces.Services.Blog;
using Business.Mappings.Blog;
using Business.Models.Blog.Dtos;
using Business.Models.Blog.Recipe;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Business.Services.Blog;

public class RecipePostService : MainService, IRecipePostService
{
    private readonly Guid blogId = Guid.Parse("2a2ff613-6f3b-4dd8-9fd6-a2f824b67b62");
    private readonly IRecipePostRepository _repository;
    private readonly IHttpContextAccessor _httpAccessor;
    private readonly IValidator<RecipePostAddDto> _addValidator;
    public RecipePostService(IRecipePostRepository repository, IHttpContextAccessor httpAccessor, IValidator<RecipePostAddDto> addValidator)
    {
        _repository = repository;
        _httpAccessor = httpAccessor;
        _addValidator = addValidator;
    }

    #region Public Methods
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
    #endregion

    #region Authenticated Methods
    public async Task AddRecipe(RecipePostAddDto recipe)
    {
        try
        
        {
            var validationResult = await _addValidator.ValidateAsync(recipe);
            if (!validationResult.IsValid)
            {
                AddProcessingError(validationResult);
                return;
            }
            var recipeDb = await _repository.GetRecipeByTitle(recipe.Title);
            if (recipeDb != null)
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
    #endregion

    #region Admin Methods
    #endregion
}
