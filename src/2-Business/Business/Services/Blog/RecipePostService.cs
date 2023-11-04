using Business.Helpers.Auth;
using Business.Interfaces.Repositories.Blog;
using Business.Interfaces.Services.Blog;
using Business.Mappings.Blog;
using Business.Models.Blog.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Business.Services.Blog;

public class RecipePostService : MainService, IRecipePostService
{
    private readonly Guid blogId = Guid.Parse("2a2ff613-6f3b-4dd8-9fd6-a2f824b67b62");
    private readonly IRecipePostRepository _repository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IHttpContextAccessor _httpAccessor;
    private readonly IValidator<RecipePostAddDto> _addValidator;
    public RecipePostService(IRecipePostRepository repository, ICategoryRepository categoryRepository, IHttpContextAccessor httpAccessor, IValidator<RecipePostAddDto> addValidator)
    {
        _repository = repository;
        _categoryRepository = categoryRepository;
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
    public async Task<RecipePostViewDto> GetRecipeByIdForCurrentUser(Guid id)
    {
        try
        {
            var userAuthenticated = AuthHelper.GetUserId(_httpAccessor).ToString();
            var recipeDb = await _repository.GetRecipeByIdAndUser(id, userAuthenticated);
            if (recipeDb == null || recipeDb.UserId != userAuthenticated)
            {
                AddProcessingError("Falha ao buscar receita: Receita não encontrada.");
                return null!;
            };
            return recipeDb.ToDto();
        }
        catch (Exception ex)
        {
            AddProcessingError($"Falha ao buscar receita: {ex.Message}");
            return null!;
        }
    }

    public async Task<IEnumerable<RecipePostViewDto>> GetRecipesForCurrentUser()
    {
        try
        {
            var userAuthenticated = AuthHelper.GetUserId(_httpAccessor).ToString();
            var recipesDb = await _repository.GetAllRecipesByUser(userAuthenticated);
            if (recipesDb == null || recipesDb.All(r => r.UserId != userAuthenticated))
            {
                AddProcessingError("Falha ao buscar receitas: Receitas não encontradas.");
                return null!;
            };
            return recipesDb.Select(x => x.ToDto());
        }
        catch (Exception ex)
        {
            AddProcessingError($"Falha ao buscar receita: {ex.Message}");
            return null!;
        }
    }
    public async Task AddRecipe(RecipePostAddDto recipe)
    {
        try
        
        {
            if (!IsWriter())
            {
                AddProcessingError("Erro ao adicionar receita: Usuário não possui permissão de escritor.");
                return;
            }
            var validationResult = await _addValidator.ValidateAsync(recipe);
            if (!validationResult.IsValid)
            {
                AddProcessingError(validationResult);
                return;
            }
            if (!await CategoryExists(recipe.CategoryId))
            {
                AddProcessingError("Erro ao adicionar receita: Categoria não encontrada.");
                return;
            }
            if (await TitleExists(recipe.Title))
            {
                AddProcessingError("Erro ao adicionar receita: Título já existe");
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
            if (!IsWriter())
            {
                AddProcessingError("Erro ao deletar receita: Usuário não possui permissão de escritor.");
                return;
            }
            var userAuthenticated = AuthHelper.GetUserId(_httpAccessor).ToString();
            var recipeDb = await _repository.GetRecipeById(id);
            if (recipeDb == null || recipeDb.UserId != userAuthenticated)
            {
                AddProcessingError("Falha ao deletar receita: Receita não encontrada.");
                return;
            };
            await _repository.DeleteAsync(id);
            return;

        }
        catch (Exception ex)
        {
            AddProcessingError($"Falha ao deletar receita: {ex.Message}");
            return;
        }
    }

    public async Task UpdateRecipe(Guid recipeId, RecipePostAddDto recipe)
    {
        try
        {
            var userAuthenticated = AuthHelper.GetUserId(_httpAccessor).ToString();
            var recipeDb = await _repository.GetRecipeById(recipeId);
            if (recipeDb == null || recipeDb.UserId != userAuthenticated || !await RecipeExists(recipeId))
            {
                AddProcessingError("Falha ao atualizar receita: Receita não encontrada.");
                return;
            };
            if (!IsWriter())
            {
                AddProcessingError("Erro ao atualizar receita: Usuário não possui permissão de escritor.");
                return;
            }
            if (!await CategoryExists(recipe.CategoryId))
            {
                AddProcessingError("Erro ao atualizar receita: Categoria não encontrada.");
                return;
            }
            if (!recipeDb.Title.Equals(recipe.Title))
            {
                if (await TitleExists(recipe.Title))
                {
                    AddProcessingError("Erro ao atualizar receita: Título já existe");
                    return;
                }
            }
            recipeDb.Title = recipe.Title;
            recipeDb.Content = recipe.Content;
            recipeDb.PreparationTime = recipe.PreparationTime;
            recipeDb.Difficulty = recipe.Difficulty;
            recipeDb.Servings = recipe.Servings;
            recipeDb.Ingredients = recipe.Ingredients.ToString();
            recipeDb.CategoryId = recipe.CategoryId;
            recipeDb.GenerateURL();
            await _repository.UpdateAsync(recipeDb);
            return;

        }
        catch (Exception ex)
        {
            AddProcessingError($"Falha ao atualizar receita: {ex.Message}");
            return;
        }
    }
    #endregion

    #region Verification Methods
    private bool IsWriter()
    {
        return AuthHelper.UserHasClaim(_httpAccessor, "Permission", "Writer");
    }
    private async Task<bool> CategoryExists(Guid categoryId)
    {
        return await _categoryRepository.GetByIdAsync(categoryId) != null ? true : false;
    }
    private async Task<bool> TitleExists(string title)
    {
        return await _repository.GetRecipeByTitle(title) != null ? true : false;
    }
    private async Task<bool> RecipeExists(Guid recipeId)
    {
        return await _repository.GetRecipeById(recipeId) != null ? true : false;
    }
    #endregion
}
