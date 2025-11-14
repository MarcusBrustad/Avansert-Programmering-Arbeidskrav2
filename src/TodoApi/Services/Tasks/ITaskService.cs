using TodoApi.DTOs.Tasks;

namespace TodoApi.Services.Tasks;

public interface ITaskService
{
    Task<IEnumerable<TaskResponseDto>> GetAllByUserIdAsync(Guid userId);

    Task<TaskResponseDto> CreateAsync(CreateTaskDto dto, Guid userId);
    
    Task<TaskResponseDto?> UpdateAsync(Guid taskId, UpdateTaskDto dto, Guid userId);
    
    Task<TaskResponseDto?> DeleteAsync(Guid taskId, Guid userId);
}