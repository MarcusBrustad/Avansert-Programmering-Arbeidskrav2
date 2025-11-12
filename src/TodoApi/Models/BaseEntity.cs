using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models;

public class BaseEntity
{
    [Key]
    public Guid Id { get; set; } 
    public DateTime CreatedAt { get; set; }
}