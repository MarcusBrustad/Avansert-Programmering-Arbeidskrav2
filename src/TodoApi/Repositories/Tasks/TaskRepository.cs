using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Repositories.Tasks;

public class TaskRepository(TodoApiDbContext dbContext) : BaseRepository<TodoTask>(dbContext), ITaskRepository
{
    public async Task<IEnumerable<TodoTask>> GetAllByUserIdAsync(Guid userId)
    {
        return await _context.TodoTasks.Where(t => t.UserId == userId).ToListAsync();
    }
}