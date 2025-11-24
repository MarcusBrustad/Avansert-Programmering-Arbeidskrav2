namespace TodoApi.Exceptions;

public sealed class GoneException(string message) : Exception(message)
{
    
}