using Business.Helpers;
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
    public async Task<RecipePostViewDto> GetRecipeById(Guid id, Guid? userId = null)
    {
        try
        {
            if (userId != null && userId != Guid.Empty)
            {
                var userAuthenticated = AuthHelper.GetUserId(_httpAccessor).ToString();
                if (Guid.Parse(userAuthenticated) != userId)
                {
                    AddProcessingError("Falha ao buscar receita: Usuário não autenticado.");
                    AddProcessingError("Falha ao buscar receita: Receita não encontrada.");
                    return null!;
                }
                var recipeUserDb = await _repository.GetRecipeById(id, userId);
                if (recipeUserDb == null)
                {
                    AddProcessingError("Falha ao buscar receita: Receita não encontrada.");
                    return null!;
                }
                return recipeUserDb.ToDto();
            }
            var recipeDb = await _repository.GetRecipeById(id);

            if (recipeDb == null)
            {
                AddProcessingError("Falha ao buscar receita: Receita não encontrada.");
                return null!;
            }
            return recipeDb.ToDto();
        }
        catch (Exception ex)
        {
            AddProcessingError($"Falha ao buscar receita: {ex.Message}");
            return null!;
        }
    }

    public async Task<RecipePostViewDto> GetRecipeByUrl(string url, Guid? userId = null)
    {
        try
        {
            if (userId != null && userId != Guid.Empty)
            {
                var userAuthenticated = AuthHelper.GetUserId(_httpAccessor).ToString();
                if (Guid.Parse(userAuthenticated) != userId)
                {
                    AddProcessingError("Falha ao buscar receita: Usuário não autenticado.");
                    AddProcessingError("Falha ao buscar receita: Receita não encontrada.");
                    return null!;
                }
                var recipeUserDb = await _repository.GetRecipeByUrl(url, userId);
                if (recipeUserDb == null)
                {
                    AddProcessingError("Falha ao buscar receita: Receita não encontrada.");
                    return null!;
                }
                return recipeUserDb.ToDto();
            }
            var recipeDb = await _repository.GetRecipeByUrl(url);

            if (recipeDb == null)
            {
                AddProcessingError("Falha ao buscar receita: Receita não encontrada.");
                return null!;
            }
            return recipeDb.ToDto();
        }
        catch (Exception ex)
        {
            AddProcessingError($"Falha ao buscar receita: {ex.Message}");
            return null!;
        }
    }


    public async Task<IEnumerable<RecipePostViewDto>> GetRecipes(Guid? userId = null)
    {
        try
        {
            if(userId != null && userId != Guid.Empty)
            {
                var userAuthenticated = AuthHelper.GetUserId(_httpAccessor).ToString();
                if(Guid.Parse(userAuthenticated) != userId)
                {
                    AddProcessingError("Falha ao buscar receita: Usuário não autenticado.");
                    AddProcessingError("Falha ao buscar receitas: Nenhuma receita encontrada.");
                    return null!;
                }
                return (await _repository.GetAllRecipes(userId)).Select(x => x.ToDto());
            }
            return (await _repository.GetAllRecipes()).Select(x => x.ToDto());
        }
        catch (Exception ex)
        {
            AddProcessingError($"Falha ao buscar receita: {ex.Message}");
            return null!;
        }
    }

    public async Task<IEnumerable<RecipePostViewDto>> GetRecipesBySearch(string searchQuery, Guid? userId = null)
    {
        try
        {
            if (userId != null && userId != Guid.Empty)
            {
                var userAuthenticated = AuthHelper.GetUserId(_httpAccessor).ToString();
                if (Guid.Parse(userAuthenticated) != userId)
                {
                    AddProcessingError("Falha ao buscar receita: Usuário não autenticado.");
                    AddProcessingError("Falha ao buscar receitas: Nenhuma receita encontrada.");
                    return null!;
                }
                return (await _repository.GetRecipesBySearch(searchQuery, userId)).Select(x => x.ToDto());
            }
            return (await _repository.GetRecipesBySearch(searchQuery)).Select(x => x.ToDto());
        }
        catch (Exception ex)
        {
            AddProcessingError($"Falha ao buscar receita: {ex.Message}");
            return null!;
        }
    }

    public async Task<IEnumerable<RecipePostViewDto>> GetRecipesByCategory(string category, Guid? userId = null)
    {
        try
        {
            if (userId != null && userId != Guid.Empty)
            {
                var userAuthenticated = AuthHelper.GetUserId(_httpAccessor).ToString();
                if (Guid.Parse(userAuthenticated) != userId)
                {
                    AddProcessingError("Falha ao buscar receita: Usuário não autenticado.");
                    AddProcessingError("Falha ao buscar receitas: Nenhuma receita encontrada.");
                    return null!;
                }
                return (await _repository.GetRecipesByCategory(category, userId)).Select(x => x.ToDto());
            }
            return (await _repository.GetRecipesByCategory(category)).Select(x => x.ToDto());
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

            recipe.Image.Name = recipeMapped.Id + "_" + recipe.Image.Name;
            recipeMapped.CoverImage = recipe.Image.Name;
            recipeMapped.BlogId = blogId;
            recipeMapped.UserId = AuthHelper.GetUserId(_httpAccessor).ToString();
            recipeMapped.GenerateURL();
            if (!ImageHelper.UploadImage(recipe.Image))
            {
                AddProcessingError("Falha na imagem");
                return;
            }
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
            recipeDb.PreparationSteps = recipe.PreparationSteps;
            recipeDb.PreparationTime = recipe.PreparationTime;
            recipeDb.Difficulty = recipe.Difficulty;
            recipeDb.Servings = recipe.Servings;
            recipeDb.Ingredients = recipe.Ingredients;
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
