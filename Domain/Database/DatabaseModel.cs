using Domain.Entities.Armor.Models;
using Domain.ValueObjects;
using Domain.Entities.Weapons.Models;

namespace Domain.Database;

public class DatabaseModel
{
    public List<UserAccount> Users { get; set; } = new();

    public List<PlayerProgress> Progress { get; set; } = new();

    public List<MonsterProfile> Monsters { get; set; } = new();

    public List<WandEntity> Wands { get; set; } = new();

    public List<StaffEntity> Staff { get; set; } = new();

    public List<SwordEntity> Swords { get; set; } = new();

    public List<AxeEntity> Axes { get; set; } = new();

    public List<BootsEntity> Boots { get; set; } = new();

    public List<ChestEntity> Chests { get; set; } = new();

    public List<GlovesEntity> Gloves { get; set; } = new();

    public List<HelmetEntity> Helmets { get; set; } = new();

    public List<LegsEntity> Legs { get; set; } = new();

    public static DatabaseModel CreateDefault()
    {
        var database = new DatabaseModel();
        database.Monsters.AddRange(CreateDefaultMonsters());
        return database;
    }

    public static IEnumerable<MonsterProfile> CreateDefaultMonsters()
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
