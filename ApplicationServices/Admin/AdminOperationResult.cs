namespace ApplicationServices.Admin;

public class AdminOperationResult<T>
{
    private AdminOperationResult()
    {
    }

    public bool Success { get; private init; }

    public T? Data { get; private init; }

    public string? Error { get; private init; }

    public AdminErrorType ErrorType { get; private init; }

    public static AdminOperationResult<T> FromSuccess(T data) => new()
    {
        Success = true,
        Data = data,
        ErrorType = AdminErrorType.None
    };

    public static AdminOperationResult<T> Unauthorized(string error) => new()
    {
        Success = false,
        Error = error,
        ErrorType = AdminErrorType.Unauthorized
    };

    public static AdminOperationResult<T> NotFound(string error) => new()
    {
        Success = false,
        Error = error,
        ErrorType = AdminErrorType.NotFound
    };

    public static AdminOperationResult<T> ValidationFailed(string error) => new()
    {
        Success = false,
        Error = error,
        ErrorType = AdminErrorType.Validation
    };

    public static AdminOperationResult<T> Conflict(string error) => new()
    {
        Success = false,
        Error = error,
        ErrorType = AdminErrorType.Conflict
    };
}
