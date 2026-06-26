namespace ERP.SharedKernel.Results;

public sealed record Error(string Code, string Message)
{
    public static readonly Error None = new(string.Empty, string.Empty);

    public static Error NotFound(string resource, object id) =>
        new($"{resource}.NotFound", $"{resource} with id '{id}' was not found.");

    public static Error Validation(string field, string message) =>
        new($"{field}.Validation", message);

    public static Error Unauthorized(string message = "Unauthorized access.") =>
        new("Auth.Unauthorized", message);

    public static Error Conflict(string resource, string message) =>
        new($"{resource}.Conflict", message);
}
