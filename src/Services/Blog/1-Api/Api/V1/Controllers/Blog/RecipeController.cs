using Api.Configuration.Filter;
using Api.Controllers.Configuration.Response;
using Asp.Versioning;
using Business.Interfaces.Services.Blog;
using Business.Models.Blog.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.V1.Controllers.Blog;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/recipes")]
public class RecipeController : MainController
{
    private readonly IRecipePostService _service;
    public RecipeController(IRecipePostService service)
    {
        _service = service;
    }

    #region Public Methods
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRecipeById(Guid id, [FromQuery] Guid? userId)
    {
        var result = await _service.GetRecipeById(id, userId);
        return _service.IsOperationValid() ? CustomResponse(result) : CustomResponse(_service.GetErrors());
    }

    [HttpGet("url/{url}")]
    public async Task<IActionResult> GetRecipeByUrl(string url, [FromQuery] Guid? userId)
    {
        var result = await _service.GetRecipeByUrl(url, userId);
        return _service.IsOperationValid() ? CustomResponse(result) : CustomResponse(_service.GetErrors());
    }

    [HttpGet("search/{search}")]
    public async Task<IActionResult> GetRecipesBySearch([FromRoute] string search, [FromQuery] Guid? userId)
    {
        var result = await _service.GetRecipesBySearch(search, userId);
        return _service.IsOperationValid() ? CustomResponse(result) : CustomResponse(_service.GetErrors());
    }

    [HttpGet("category/{category}")]
    public async Task<IActionResult> GetRecipesByCategory([FromRoute] string category, [FromQuery] Guid? userId)
    {
        Console.WriteLine(category);
        var result = await _service.GetRecipesByCategory(category, userId);
        return _service.IsOperationValid() ? CustomResponse(result) : CustomResponse(_service.GetErrors());
    }

    [HttpGet()]
    public async Task<IActionResult> GetRecipes([FromQuery] Guid? userId)
    {
        var result = await _service.GetRecipes(userId);
        return _service.IsOperationValid() ? CustomResponse(result) : CustomResponse(_service.GetErrors());
    }
    #endregion

    #region Authenticated Methods
    [Authorize]
    [ClaimsAuthorize("Permission", "Writer")]
    [HttpPost("/api/admin/recipes")]
    public async Task<IActionResult> PostRecipe(RecipePostAddDto recipe)
    {
        await _service.AddRecipe(recipe);
        return _service.IsOperationValid() ? CustomResponse() : CustomResponse(_service.GetErrors());
    }

    [Authorize]
    [ClaimsAuthorize("Permission", "Writer")]
    [HttpPut("/api/admin/recipes/{id}")]
    public async Task<IActionResult> PutRecipe(Guid id, RecipePostAddDto recipe)
    {
        await _service.UpdateRecipe(id, recipe);
        return _service.IsOperationValid() ? CustomResponse() : CustomResponse(_service.GetErrors());
    }

    [Authorize]
    [ClaimsAuthorize("Permission", "Writer")]
    [HttpDelete("/api/admin/recipes/{id}")]
    public async Task<IActionResult> DeleteRecipe(Guid id)
    {
        await _service.DeleteRecipe(id);
        return _service.IsOperationValid() ? CustomResponse() : CustomResponse(_service.GetErrors());
    }
    #endregion
}
