using Domain.ValueObjects;

namespace Domain.Database;

public static partial class DbCreateDefault
{
    public static partial class Database
    {
        public static IEnumerable<MonsterProfile> MonsterProfiles()
        {
            return new List<MonsterProfile>
            {
                new()
                {
                    Name = "Forest Goblin",
                    Description = "A sneaky goblin lurking in the underbrush.",
                    Level = 1,
                    HitPoints = 18,
                    AttackPower = 4
                },
                new()
                {
                    Name = "Cavern Bat",
                    Description = "A swift bat that attacks from above.",
                    Level = 2,
                    HitPoints = 22,
                    AttackPower = 6
                },
                new()
                {
                    Name = "Stone Golem",
                    Description = "A towering golem guarding the ancient ruins.",
                    Level = 5,
                    HitPoints = 60,
                    AttackPower = 12
                },
                new()
                {
                    Name = "Marsh Serpent",
                    Description = "A venomous serpent that strikes from the reeds.",
                    Level = 3,
                    HitPoints = 30,
                    AttackPower = 8
                },
                new()
                {
                    Name = "Bandit Captain",
                    Description = "A seasoned raider leading a small gang.",
                    Level = 4,
                    HitPoints = 42,
                    AttackPower = 10
                }
            };
        }
    }
}
