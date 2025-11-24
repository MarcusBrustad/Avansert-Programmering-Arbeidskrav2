namespace TodoApi.Exceptions;

public readonly record struct ValidationError(string PropertyName, string ErrorMessage);