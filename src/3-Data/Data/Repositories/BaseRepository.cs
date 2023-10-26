using Business.Interfaces.Repositories;
using Business.Models.Blog;
using Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : Entity
{
    protected readonly ApplicationDbContext _context;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entityDb = await GetByIdAsync(id);
        _context.Set<T>().Remove(entityDb);
        await _context.SaveChangesAsync();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        var entityDb = await _context.Set<T>().FindAsync(id);
        if (entityDb == null) return null!;
        return entityDb;
    }

    public async Task UpdateAsync(T entity)
    {
        entity.UpdatedAt = DateTime.Now;
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }
}
