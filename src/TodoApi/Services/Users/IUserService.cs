using TodoApi.DTOs.Users;
using TodoApi.Models;

namespace TodoApi.Services.Users;

public interface IUserService
{
    Task<UserResponseDto?> RegisterAsync(RegisterUserDto dto);
    Task<UserResponseDto?> GetByIdAsync(Guid id);
    
    Task<User?> AuthenticateUserAsync(string username, string password); 
}