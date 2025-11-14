using TodoApi.DTOs.Users;
using TodoApi.Models;

namespace TodoApi.Mappers.Users;

public class UserResponseMapper : IMapper<UserResponseDto, User>
{
    public UserResponseDto MapToDto(User model)
    {
        return new UserResponseDto
        {
            Username = model.Username,
            Id = model.Id,
            CreatedAt = model.CreatedAt,

        };
    }

    public User MapToModel(UserResponseDto dto)
    {
        throw new NotImplementedException();
    }
}