using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOs.Tasks;

public class CreateTaskDto
{
    [Required]
    [MaxLength(50)]
    public string Title { get; set; } = string.Empty;
    
    [MaxLength(500)]
    public string? Description { get; set; }
    
    public DateTime? DueDate { get; set; }
}