using Api.Controllers.Configuration.Response;
using Asp.Versioning;
using Business.Interfaces.Services;
using Business.Interfaces.Services.Blog;
using Business.Models.Blog;
using Microsoft.AspNetCore.Mvc;

namespace Api.V1.Controllers.Blog;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/blogs")]
public class BlogController : MainController
{
    private readonly IBlogService _service;
    public BlogController(IBlogService service, INotifier notifier) : base(notifier)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBlogById(Guid id)
    {
        var result = await _service.GetBlogById(id);
        return CustomResponse(result);
    }

    [HttpGet()]
    public async Task<IActionResult> GetAllBlogs()
    {
        var result = await _service.GetAllBlogs();
        return CustomResponse(result);
    }

    [HttpPost("/api/admin/blogs")]
    public async Task<IActionResult> PostBlog(BlogEntity blog)
    {
        await _service.AddBlog(blog);
        return CustomResponse();
    }

    [HttpPut("/api/admin/blogs/{id}")]
    public async Task<IActionResult> PutBlog(Guid id, BlogEntity blog)
    {
        await _service.UpdateBlog(id, blog);
        return CustomResponse();
    }

    [HttpDelete("/api/admin/blogs/{id}")]
    public async Task<IActionResult> DeleteBlog(Guid id)
    {
        await _service.DeleteBlog(id);
        return CustomResponse();
    }
}
