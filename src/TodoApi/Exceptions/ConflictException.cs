namespace TodoApi.Exceptions;

public sealed class ConflictException(string message) : Exception(message)
{
    
}