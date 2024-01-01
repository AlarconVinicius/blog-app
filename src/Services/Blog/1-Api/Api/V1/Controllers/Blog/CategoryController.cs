using Api.Controllers.Configuration.Response;
using Asp.Versioning;
using Business.Interfaces.Services;
using Business.Interfaces.Services.Blog;
using Business.Models.Blog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.V1.Controllers.Blog;

[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/categories")]
public class CategoryController : MainController
{
    private readonly ICategoryService _service;
    public CategoryController(ICategoryService service, INotifier notifier) : base(notifier)
    {
        _service = service;
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        var result = await _service.GetCategoryById(id);
        return CustomResponse(result);
    }

    [AllowAnonymous]
    [HttpGet()]
    public async Task<IActionResult> GetAllCategoryCategories()
    {
        var result = await _service.GetAllCategories();
        return CustomResponse(result);
    }

    [HttpPost("categories")]
    public async Task<IActionResult> PostCategory(Category category)
    {
        await _service.AddCategory(category);
        return CustomResponse();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategory(Guid id, Category category)
    {
        await _service.UpdateCategory(id, category);
        return CustomResponse();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        await _service.DeleteCategory(id);
        return CustomResponse();
    }
}
