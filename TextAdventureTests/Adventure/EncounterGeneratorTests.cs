using ApplicationServices.Adventure;
using ApplicationServices.Contracts.Repositories;
using ApplicationServices.Contracts.Services;
using System;
using System.Linq;
using Domain.Core;
using Domain.ValueObjects;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using Shouldly;

namespace TextAdventureTests.Adventure;

public class EncounterGeneratorTests
{
    [Fact]
    public async Task GenerateForTravel_UsesRangesAndAddsCoinLoot()
    {
        var random = new FixedRandomService();
        var repository = Substitute.For<IWorldRepository>();

        var monster = new Monster
        {
            Id = "ashen_construct",
            Name = "Ashen Construct",
            Biome = "Mountain",
            LevelRange = new MonsterStatRange { Min = 6, Max = 10 },
            HitPointRange = new MonsterStatRange { Min = 30, Max = 40 },
            AttackRange = new MonsterStatRange { Min = 8, Max = 10 },
            DefenseRange = new MonsterStatRange { Min = 4, Max = 6 },
            CoinDropRange = new MonsterStatRange { Min = 5, Max = 5 },
            PreferredThreatLevels = ["Low", "High"]
        };

        repository.GetMonstersAsync().Returns(new List<Monster> { monster });
        repository.GetDropTablesAsync().Returns(new List<DropTable>
        {
            new()
            {
                Biome = "Mountain",
                Drops = ["iron_shield", "ore_fragment"]
            }
        });

        var generator = new EncounterGenerator(random, repository, NullLogger<EncounterGenerator>.Instance);
        var character = new Character { Id = Guid.NewGuid(), Level = 3 };
        var origin = new WorldLocationNode { Id = "old_watchtower", Name = "Old Watchtower" };
        var destination = new WorldLocationNode
        {
            Id = "highridge_pass",
            Name = "Highridge Pass",
            Biome = "Mountain",
            ThreatLevel = "Low"
        };

        var result = await generator.GenerateForTravelAsync(character, origin, destination);

        result.ShouldNotBeNull();
        result!.Encounter.MonsterId.ShouldBe(monster.Id);

        var loot = result.Loot.ToList();
        loot.ShouldContain(l => l.ItemId == "iron_shield");
        var coins = loot.Single(l => l.ItemId == "coins");
        coins.Quantity.ShouldBeGreaterThan(0);
        coins.Quantity.ShouldBeLessThanOrEqualTo(5); // clamped by threat level range
    }

    private sealed class FixedRandomService : IRandomService
    {
        public int NextInt(int minInclusive, int maxExclusive) => minInclusive;

        public double NextDouble() => 0.1;

        public byte[] GetBytes(int length) => Enumerable.Repeat((byte)1, length).ToArray();
    }
}
