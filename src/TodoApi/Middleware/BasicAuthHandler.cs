using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using TodoApi.Models;
using TodoApi.Services.Users;

namespace TodoApi.Middleware;

public class BasicAuthHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options, 
    ILoggerFactory logger,
    UrlEncoder encoder,
    IUserService userService) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        try
        {
            var authHeader = Request.Headers["Authorization"].ToString();
            
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Basic "))
                return AuthenticateResult.Fail("Missing Authorization Header");
            
            var base64Part = authHeader["Basic ".Length..];
            var credentialBytes = Convert.FromBase64String(base64Part);

            var credentials = System.Text.Encoding.UTF8.GetString(credentialBytes).Split([':'], 2);
            
            var username = credentials[0];
            var password = credentials[1];

            User? user = await userService.AuthenticateUserAsync(username, password);
            if (user == null) return AuthenticateResult.Fail("Invalid username or password");

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            
            return AuthenticateResult.Success(ticket);
            
        }
        catch (Exception)
        {
            return AuthenticateResult.Fail("Missing or invalid Authorization header");
        }
    }
}