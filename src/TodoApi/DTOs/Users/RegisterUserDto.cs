using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOs.Users;

public class RegisterUserDto
{
    [Required] [MaxLength(50)] 
    public string Username { get; set; } = string.Empty;
    
    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;
}