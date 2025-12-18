using Domain.Core;
using Shouldly;

namespace TextAdventureTests.Characters;

public class LevelingSystemTests
{
    [Fact]
    public void ApplyExperience_LevelsUpAtThresholdAndRaisesStats()
    {
        var character = new Character
        {
            Level = 1,
            Experience = 0,
            Stats = new CharacterStats { Combat = 2, Stealth = 2, Pickpocket = 2 }
        };

        var result = LevelingSystem.ApplyExperience(character, 200);

        result.LeveledUp.ShouldBeTrue();
        result.LevelsGained.ShouldBe(1);
        character.Level.ShouldBe(2);
        character.Experience.ShouldBe(200);
        character.Stats.Combat.ShouldBe(3);
        character.Stats.Stealth.ShouldBe(3);
        character.Stats.Pickpocket.ShouldBe(3);
    }

    [Fact]
    public void ApplyExperience_AllowsMultipleLevelUps()
    {
        var character = new Character
        {
            Level = 1,
            Experience = 0,
            Stats = new CharacterStats { Combat = 2, Stealth = 2, Pickpocket = 2 }
        };

        var result = LevelingSystem.ApplyExperience(character, 1200);

        result.LevelsGained.ShouldBe(2);
        character.Level.ShouldBe(3);
        character.Experience.ShouldBe(1200);
        character.Stats.Combat.ShouldBe(4);
        character.Stats.Stealth.ShouldBe(4);
        character.Stats.Pickpocket.ShouldBe(4);
    }

    [Fact]
    public void GetExperienceForLevel_ScalesBeyondDefinedTable()
    {
        LevelingSystem.GetExperienceForLevel(9).ShouldBe(26000);
    }
}
