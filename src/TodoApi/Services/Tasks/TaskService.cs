using Microsoft.AspNetCore.Http.HttpResults;
using TodoApi.Auth;
using TodoApi.DTOs.Tasks;
using TodoApi.Exceptions;
using TodoApi.Mappers;
using TodoApi.Models;
using TodoApi.Repositories.Tasks;

namespace TodoApi.Services.Tasks;

public class TaskService(ITaskRepository taskRepository,
    IMapper<CreateTaskDto, TodoTask> createTaskMapper,
    IMapper<TaskResponseDto, TodoTask> taskResponseMapper,
    ICurrentUser currentUser) : ITaskService
{
    
    public async Task<IEnumerable<TaskResponseDto>> GetAllAsync()
    {
        if (currentUser.UserId == null)
            throw new UnauthorizedException("User is not authenticated");
     
        var userId = currentUser.UserId.Value;
        
        IEnumerable<TodoTask> tasks =  await taskRepository.GetAllByUserIdAsync(userId);
        
        return tasks.Select(taskResponseMapper.MapToDto);
    }
    
    
    public async Task<TaskResponseDto> CreateAsync(CreateTaskDto dto)
    {
        if (currentUser.UserId == null)
            throw new UnauthorizedException("User is not authenticated");
     
        var userId = currentUser.UserId.Value;
        
        TodoTask task = createTaskMapper.MapToModel(dto);
        task.Id = Guid.NewGuid();
        task.UserId = userId;
        task.CreatedAt = DateTime.UtcNow;
        
        TodoTask addedTask = await taskRepository.AddAsync(task);

        return taskResponseMapper.MapToDto(addedTask);
    }

    public async Task<TaskResponseDto> UpdateAsync(Guid taskId, UpdateTaskDto dto)
    {
        if (currentUser.UserId == null)
            throw new UnauthorizedException("User is not authenticated");
        
        var existingTask = await taskRepository.GetByIdAsync(taskId);
        if (existingTask.UserId != currentUser.UserId)
            throw new ForbiddenException("You are not authorized to update this task");
        
        if (dto.Title != null)
            existingTask.Title = dto.Title;
        if (dto.Description != null)
            existingTask.Description = dto.Description;
        if (dto.DueDate != null)
            existingTask.DueDate = dto.DueDate;
        if (dto.IsCompleted.HasValue)
            existingTask.IsCompleted = dto.IsCompleted.Value;
    
        
        TodoTask updatedTask = await taskRepository.UpdateAsync(existingTask);
        return taskResponseMapper.MapToDto(updatedTask);
    }

    public async Task<TaskResponseDto> DeleteAsync(Guid taskId)
    {
        if (currentUser.UserId == null)
            throw new UnauthorizedException("User is not authenticated");
        
        var existingTask = await taskRepository.GetByIdAsync(taskId);
        if (existingTask.UserId != currentUser.UserId)
            throw new ForbiddenException("You are not authorized to delete this task");
        
        TodoTask deletedTask = await taskRepository.DeleteByIdAsync(taskId);
        return taskResponseMapper.MapToDto(deletedTask);
    }
}