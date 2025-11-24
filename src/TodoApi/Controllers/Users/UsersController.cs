using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Auth;
using TodoApi.DTOs.Users;
using TodoApi.Services.Users;

namespace TodoApi.Controllers.Users;

[ApiController]
[Route("api/v1/users")]
public class UsersController(IUserService userService, ICurrentUser currentUser) : ControllerBase
{
    
    //[Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<UserResponseDto>> GetCurrentUser()
    {
        var userId = currentUser.UserId;

        if (userId == null)
        {
            return Unauthorized();
        }
        
        
        var user = await userService.GetByIdAsync(userId.Value);
        return user is null
            ? NotFound("User not found")
            : Ok(user);
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserResponseDto>> RegisterUserAsync(
        [FromBody] RegisterUserDto user)
    {
        Console.WriteLine($"Register called with username: {user.Username}");
        
        var newUser = await userService.RegisterAsync(user);
        
        Console.WriteLine($"Service returned: {(newUser == null ? "null" : "user object")}");
        
        return newUser is null
            ? Conflict("Username already exists")
            : Created("User created successfully",newUser);
    }
    
}