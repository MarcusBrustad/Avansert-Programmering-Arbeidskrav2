using TodoApi.Models;

namespace TodoApi.Repositories.Tasks;

public interface ITaskRepository : IBaseRepository<TodoTask>
{
    Task<IEnumerable<TodoTask>> GetAllByUserIdAsync(Guid userId);
    
}