using TodoApi.DTOs.Tasks;
using TodoApi.Models;

namespace TodoApi.Mappers.Tasks;

public class CreateTaskMapper : IMapper<CreateTaskDto, TodoTask>
{
    public CreateTaskDto MapToDto(TodoTask model)
    {
        throw new NotImplementedException();
    }

    public TodoTask MapToModel(CreateTaskDto dto)
    {
        return new TodoTask
        {
            Title = dto.Title,
            Description = dto.Description,
            DueDate = dto.DueDate
        };
    }
}