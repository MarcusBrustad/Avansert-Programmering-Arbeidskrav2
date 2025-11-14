using TodoApi.DTOs.Users;
using TodoApi.Mappers;
using TodoApi.Mappers.Users;
using TodoApi.Models;
using TodoApi.Repositories.Users;

namespace TodoApi.Services.Users;

public class UserService(IUserRepository userRepository,
    IMapper<UserResponseDto, User> userResponseMapper,
    IMapper<RegisterUserDto, User> registerUserMapper) : IUserService
{
    public async Task<UserResponseDto?> RegisterAsync(RegisterUserDto dto)
    {
        var existingUser = await userRepository.GetByUsernameAsync(dto.Username);
        if (existingUser != null) return null;
        
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        User user = registerUserMapper.MapToModel(dto);
        user.Id = Guid.NewGuid();
        user.HashedPassword = hashedPassword;
        user.CreatedAt = DateTime.UtcNow;
        
        User? addedUser = await userRepository.AddAsync(user);
        return addedUser == null ? null : userResponseMapper.MapToDto(addedUser);
    }

    public async Task<UserResponseDto?> GetByIdAsync(Guid id)
    {
        User? existingUser = await userRepository.GetByIdAsync(id);
        return existingUser == null ? null : userResponseMapper.MapToDto(existingUser);
        
    }
}