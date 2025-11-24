namespace TodoApi.Exceptions;

public sealed class UnauthorizedException(string message = "Unauthorized") : Exception(message)
{
    
}