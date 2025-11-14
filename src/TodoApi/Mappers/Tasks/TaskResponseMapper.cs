using TodoApi.DTOs.Tasks;
using TodoApi.Models;

namespace TodoApi.Mappers.Tasks;

public class TaskResponseMapper : IMapper<TaskResponseDto, TodoTask>
{
    public TaskResponseDto MapToDto(TodoTask model)
    {
        return new TaskResponseDto
        {
            Id = model.Id,
            Title = model.Title,
            Description = model.Description,
            DueDate = model.DueDate,
            IsCompleted = model.IsCompleted,
            CreatedAt = model.CreatedAt

        };
    }

    public TodoTask MapToModel(TaskResponseDto dto)
    {
        throw new NotImplementedException();
    }
}