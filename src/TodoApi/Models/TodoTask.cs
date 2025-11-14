using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models;

public class TodoTask : BaseEntity
{
    [Required]
    [MaxLength(50)]
    public string Title { get; set; } = string.Empty;
    
    [MaxLength(500)]
    public string? Description { get; set; } 
    
    public DateTime? DueDate { get; set; }
    public bool IsCompleted { get; set; } 
    
    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    public User User { get; set; } = null!;
}