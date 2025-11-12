using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOs.Tasks;

public class UpdateTaskDto
{
    [MaxLength(50)]
    public string? Title { get; set; }
    
    [MaxLength(500)]
    public string? Description { get; set; }
    
    public DateTime? DueDate { get; set; }
    
    public bool IsCompleted { get; set; }
    
}