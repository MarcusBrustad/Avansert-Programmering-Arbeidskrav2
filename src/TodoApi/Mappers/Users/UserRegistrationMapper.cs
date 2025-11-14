using TodoApi.DTOs.Users;
using TodoApi.Models;


namespace TodoApi.Mappers.Users;

public class UserRegistrationMapper : IMapper<RegisterUserDto, User>
{
    public RegisterUserDto MapToDto(User model)
    {
        throw new NotImplementedException();
    }

    public User MapToModel(RegisterUserDto dto)
    {
        return new User
        {
            Username = dto.Username
        };
    }
}