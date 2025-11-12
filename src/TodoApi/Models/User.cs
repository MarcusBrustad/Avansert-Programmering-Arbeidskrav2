using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models;

[Index(nameof(Username), IsUnique = true)]
public class User : BaseEntity
{

    [Required]
    [MaxLength(50)] 
    public string Username { get; set; } = string.Empty;
    
    [Required]
    public string HashedPassword { get; set; } = string.Empty;
    
    public ICollection<TodoTask> Tasks { get; set; } = new HashSet<TodoTask>();
}