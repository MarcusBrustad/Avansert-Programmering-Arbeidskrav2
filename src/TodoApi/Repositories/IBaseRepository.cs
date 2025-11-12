namespace TodoApi.Repositories;

public interface IBaseRepository<T>
    where T: class
{
    Task<T?> AddAsync(T entity);
    Task<T?> UpdateAsync(T entity);
    Task<T?> GetByIdAsync(Guid id);
    Task<T?> DeleteByIdAsync(Guid id);
    
}