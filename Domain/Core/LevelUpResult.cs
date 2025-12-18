namespace Domain.Core;

public record LevelUpResult(int LevelsGained)
{
    public static LevelUpResult None { get; } = new(0);

    public bool LeveledUp => LevelsGained > 0;
}
