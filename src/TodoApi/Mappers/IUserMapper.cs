using TodoApi.DTOs.Users;
using TodoApi.Models;

namespace TodoApi.Mappers;

public interface IUserMapper
{
    User ToUser(RegisterUserDto dto, string hashedPassword);
    UserResponseDto ToUserResponseDto(User user);
}