using TodoApi.DTOs.Tasks;

namespace TodoApi.Services.Tasks;

public interface ITaskService
{
    Task<IEnumerable<TaskResponseDto>> GetAllAsync();

    Task<TaskResponseDto> CreateAsync(CreateTaskDto dto);
    
    Task<TaskResponseDto> UpdateAsync(Guid taskId, UpdateTaskDto dto);
    
    Task<TaskResponseDto> DeleteAsync(Guid taskId);
}