using TodoApi.Data;
using TodoApi.Exceptions;

namespace TodoApi.Repositories;

public class BaseRepository<T>(TodoApiDbContext dbContext) : IBaseRepository<T> 
    where T: class
{
    protected readonly TodoApiDbContext _context = dbContext;
        
    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity == null)
            throw new NotFoundException(typeof(T).Name, id);
        return entity;
    }

    public async Task<T> DeleteByIdAsync(Guid id)
    {
        var entity = await _context.Set<T>().FindAsync(id);

        if (entity == null)
            throw new NotFoundException(typeof(T).Name, id);
        
        _context.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}