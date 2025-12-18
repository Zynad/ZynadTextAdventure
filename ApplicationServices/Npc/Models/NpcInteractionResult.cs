using ApplicationServices.Characters.Dto;

namespace ApplicationServices.Npc.Models;

public class NpcInteractionResult<T>
{
    private NpcInteractionResult(bool success, string? error, T? payload, CharacterStateDto? character)
    {
        Success = success;
        Error = error;
        Payload = payload;
        Character = character;
    }

    public bool Success { get; }

    public string? Error { get; }

    public T? Payload { get; }

    public CharacterStateDto? Character { get; }

    public static NpcInteractionResult<T> FromSuccess(T payload, CharacterStateDto? character)
    {
        return new NpcInteractionResult<T>(true, null, payload, character);
    }

    public static NpcInteractionResult<T> Failure(string error)
    {
        return new NpcInteractionResult<T>(false, error, default, null);
    }
}
