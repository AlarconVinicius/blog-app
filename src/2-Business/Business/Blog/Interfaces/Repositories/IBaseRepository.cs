using Business.Blog.Models;

namespace Business.Blog.Interfaces.Repositories;

public interface IBaseRepository<T> where T : Entity
{
    public Task AddAsync(T entity);

    public Task UpdateAsync(T entity);

    public Task DeleteAsync(Guid id);

    public Task<List<T>> GetAllAsync();

    public Task<T> GetByIdAsync(Guid id);
}
