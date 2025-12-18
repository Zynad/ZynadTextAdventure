using ApplicationServices.Characters.Dto;

namespace ApplicationServices.Characters.Results;

public class CharacterResult
{
    private CharacterResult(CharacterErrorType errorType, string? error, CharacterDto? character)
    {
        ErrorType = errorType;
        Error = error;
        Character = character;
    }

    public bool Success => ErrorType == CharacterErrorType.None;

    public CharacterErrorType ErrorType { get; }

    public string? Error { get; }

    public CharacterDto? Character { get; }

    public static CharacterResult FromSuccess(CharacterDto character)
    {
        return new CharacterResult(CharacterErrorType.None, null, character);
    }

    public static CharacterResult Validation(string message)
    {
        return new CharacterResult(CharacterErrorType.Validation, message, null);
    }

    public static CharacterResult Conflict(string message)
    {
        return new CharacterResult(CharacterErrorType.Conflict, message, null);
    }

    public static CharacterResult NotFound(string message)
    {
        return new CharacterResult(CharacterErrorType.NotFound, message, null);
    }

    public static CharacterResult Unauthorized(string message)
    {
        return new CharacterResult(CharacterErrorType.Unauthorized, message, null);
    }
}
