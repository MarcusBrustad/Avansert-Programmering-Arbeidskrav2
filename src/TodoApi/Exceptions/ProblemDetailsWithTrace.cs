using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Exceptions;

public sealed class ProblemDetailsWithTrace : ProblemDetails
{
    public string? TraceId { get; init; }
    
    public Dictionary<string, string[]>? Errors { get; init; }

}