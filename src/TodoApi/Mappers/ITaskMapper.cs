using TodoApi.DTOs.Tasks;
using TodoApi.Models;

namespace TodoApi.Mappers;

public interface ITaskMapper
{
    TodoTask ToTask(CreateTaskDto dto, Guid userId);
    void ApplyUpdate(TodoTask existingTask, UpdateTaskDto dto);
    TaskResponseDto ToTaskResponseDto(TodoTask task);
}