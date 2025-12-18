namespace Domain.Core;

public static class LevelingSystem
{
    private static readonly IReadOnlyList<int> ExperienceThresholds = new List<int>
    {
        0,     // Level 1
        200,   // Level 2
        1000,  // Level 3
        2500,  // Level 4
        5000,  // Level 5
        9000,  // Level 6
        14000, // Level 7
        20000  // Level 8
    };

    private const int AdditionalExperiencePerLevel = 6000;

    public static LevelUpResult ApplyExperience(Character character, int experienceGained)
    {
        if (experienceGained <= 0)
        {
            return LevelUpResult.None;
        }

        character.Stats ??= CharacterStats.Default();
        var originalLevel = character.Level;

        character.Experience += experienceGained;

        while (character.Experience >= GetExperienceForLevel(character.Level + 1))
        {
            character.Level += 1;
            ApplyStatIncrease(character.Stats);
        }

        var levelsGained = character.Level - originalLevel;
        return levelsGained > 0 ? new LevelUpResult(levelsGained) : LevelUpResult.None;
    }

    public static int GetExperienceForLevel(int level)
    {
        if (level <= 1)
        {
            return ExperienceThresholds[0];
        }

        var index = level - 1;
        if (index < ExperienceThresholds.Count)
        {
            return ExperienceThresholds[index];
        }

        var lastDefined = ExperienceThresholds[^1];
        var extraLevels = index - (ExperienceThresholds.Count - 1);
        return lastDefined + (extraLevels * AdditionalExperiencePerLevel);
    }

    private static void ApplyStatIncrease(CharacterStats stats)
    {
        stats.Combat += 1;
        stats.Stealth += 1;
        stats.Pickpocket += 1;
    }
}
