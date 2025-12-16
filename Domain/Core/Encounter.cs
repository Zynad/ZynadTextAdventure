namespace Domain.Core;

public class Encounter
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public Guid CharacterId { get; set; }
    public string MonsterId { get; set; } = string.Empty;
    public DateTimeOffset OccurredAt { get; set; } = DateTimeOffset.UtcNow;
    public string Outcome { get; set; } = string.Empty;
}
