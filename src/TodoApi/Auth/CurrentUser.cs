using System.Security.Claims;
using TodoApi.Models;

namespace TodoApi.Auth;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    public bool IsAuthenticated => User?.Identity?.IsAuthenticated == true;
    public Guid? UserId => 
        Guid.TryParse(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id) 
            ? id 
            : null;
    
    public string? UserName => User?.Identity?.Name;

    private ClaimsPrincipal? User => httpContextAccessor.HttpContext?.User;
}