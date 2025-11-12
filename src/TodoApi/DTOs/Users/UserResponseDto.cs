namespace TodoApi.DTOs.Users;

public class UserResponseDto
{
    public Guid Id { get; set; }
    
    public string Username { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; }
    
    
}