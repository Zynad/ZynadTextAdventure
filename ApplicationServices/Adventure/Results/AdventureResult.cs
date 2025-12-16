using ApplicationServices.Adventure.State;

namespace ApplicationServices.Adventure.Results;

public class AdventureResult
{
    private AdventureResult(AdventureErrorType errorType, string? error, CharacterStateDto? character)
    {
        ErrorType = errorType;
        Error = error;
        Character = character;
    }

    public bool Success => ErrorType == AdventureErrorType.None;

    public AdventureErrorType ErrorType { get; }

    public string? Error { get; }

    public CharacterStateDto? Character { get; }

    public static AdventureResult FromSuccess(CharacterStateDto character)
    {
        return new AdventureResult(AdventureErrorType.None, null, character);
    }

    public static AdventureResult Validation(string message)
    {
        return new AdventureResult(AdventureErrorType.Validation, message, null);
    }

    public static AdventureResult Conflict(string message)
    {
        return new AdventureResult(AdventureErrorType.Conflict, message, null);
    }

    public static AdventureResult NotFound(string message)
    {
        return new AdventureResult(AdventureErrorType.NotFound, message, null);
    }

    public static AdventureResult Unauthorized(string message)
    {
        return new AdventureResult(AdventureErrorType.Unauthorized, message, null);
    }
}
