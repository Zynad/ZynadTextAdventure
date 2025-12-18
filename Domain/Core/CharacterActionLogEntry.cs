namespace Domain.Core;

public class CharacterActionLogEntry
{
    public string Action { get; set; } = string.Empty;

    public string Target { get; set; } = string.Empty;

    public bool Success { get; set; }
        = false;

    public int Roll { get; set; }
        = 0;

    public int Difficulty { get; set; }
        = 0;

    public DateTimeOffset OccurredAt { get; set; }
        = DateTimeOffset.UtcNow;
}
