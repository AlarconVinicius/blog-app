using Api.Configuration.Filter;
using Api.Controllers.Configuration.Response;
using Business.Interfaces.Services.Blog;
using Business.Models.Blog.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Blog;

[Route("api/recipe-blog")]
public class RecipePostController : MainController
{
    private readonly IRecipePostService _service;
    public RecipePostController(IRecipePostService service)
    {
        _service = service;
    }

    #region Public Methods
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRecipeById(Guid id)
    {
        var result = await _service.GetRecipeById(id);
        return _service.IsOperationValid() ? CustomResponse(result) : CustomResponse(_service.GetErrors());
    }

    [HttpPost("search")]
    public async Task<IActionResult> GetRecipeBySearch([FromBody] string searchQuery)
    {
        var result = await _service.GetRecipeBySearch(searchQuery);
        return _service.IsOperationValid() ? CustomResponse(result) : CustomResponse(_service.GetErrors());
    }

    [HttpGet()]
    public async Task<IActionResult> GetRecipes()
    {
        var result = await _service.GetRecipes();
        return _service.IsOperationValid() ? CustomResponse(result) : CustomResponse(_service.GetErrors());
    }
    #endregion

    #region Authenticated Methods
    [Authorize]
    [HttpGet("admin-panel/{id}")]
    public async Task<IActionResult> GetRecipeByIdForCurrentUser(Guid id)
    {
        var result = await _service.GetRecipeByIdForCurrentUser(id);
        return _service.IsOperationValid() ? CustomResponse(result) : CustomResponse(_service.GetErrors());
    }

    [Authorize]
    [HttpGet("admin-panel")]
    public async Task<IActionResult> GetRecipesForCurrentUser()
    {
        var result = await _service.GetRecipesForCurrentUser();
        return _service.IsOperationValid() ? CustomResponse(result) : CustomResponse(_service.GetErrors());
    }

    [Authorize]
    [ClaimsAuthorize("Permission", "Writer")]
    [HttpPost]
    public async Task<IActionResult> PostRecipe(RecipePostAddDto recipe)
    {
        await _service.AddRecipe(recipe);
        return _service.IsOperationValid() ? CustomResponse() : CustomResponse(_service.GetErrors());
    }

    [Authorize]
    [ClaimsAuthorize("Permission", "Writer")]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRecipe(Guid id, RecipePostAddDto recipe)
    {
        await _service.UpdateRecipe(id, recipe);
        return _service.IsOperationValid() ? CustomResponse() : CustomResponse(_service.GetErrors());
    }

    [Authorize]
    [ClaimsAuthorize("Permission", "Writer")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRecipe(Guid id)
    {
        await _service.DeleteRecipe(id);
        return _service.IsOperationValid() ? CustomResponse() : CustomResponse(_service.GetErrors());
    }
    #endregion
}
