namespace TodoApi.Exceptions;

public sealed class ForbiddenException(string message = "Forbidden") : Exception(message)
{
    
}