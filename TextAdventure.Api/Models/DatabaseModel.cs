namespace TextAdventure.Api.Models;

public class DatabaseModel
{
    public List<UserAccount> Users { get; set; } = new();

    public List<PlayerProgress> Progress { get; set; } = new();

    public List<Monster> Monsters { get; set; } = new();

    public static DatabaseModel CreateDefault()
    {
        var database = new DatabaseModel();
        database.Monsters.AddRange(CreateDefaultMonsters());
        return database;
    }

    public static IEnumerable<Monster> CreateDefaultMonsters()
    {
        return new List<Monster>
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
            }
        };
    }
}
