using TodoApi.DTOs.Users;

namespace TodoApi.Services.Users;

public interface IUserService
{
    Task<UserResponseDto?> RegisterAsync(RegisterUserDto dto);
    Task<UserResponseDto?> GetByIdAsync(Guid id);
    
}