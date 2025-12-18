using Domain.Entities.Armor.Models;
using Domain.Entities.Items.Models;
using Domain.ValueObjects;
using Domain.Entities.Weapons.Models;

namespace Domain.Database;

public class DatabaseModel
{
    public List<UserAccount> Users { get; set; } = [];

    public List<PlayerProgress> Progress { get; set; } = [];

    public List<MonsterProfile> Monsters { get; set; } = [];

    public List<GenericItemEntity> Items { get; set; } = [];

    public List<WandEntity> Wands { get; set; } = [];

    public List<StaffEntity> Staff { get; set; } = [];

    public List<SwordEntity> Swords { get; set; } = [];

    public List<AxeEntity> Axes { get; set; } = [];

    public List<BootsEntity> Boots { get; set; } = [];

    public List<ChestEntity> Chests { get; set; } = [];

    public List<GlovesEntity> Gloves { get; set; } = [];

    public List<HelmetEntity> Helmets { get; set; } = [];

    public List<LegsEntity> Legs { get; set; } = [];

    public static DatabaseModel CreateDefault()
    {
        var database = new DatabaseModel();
        database.Monsters.AddRange(CreateDefaultMonsters());
        database.Items.AddRange(CreateDefaultItems());
        database.Helmets.AddRange(CreateDefaultHelmets());
        database.Gloves.AddRange(CreateDefaultGloves());
        database.Chests.AddRange(CreateDefaultChests());
        database.Legs.AddRange(CreateDefaultLegs());
        database.Boots.AddRange(CreateDefaultBoots());
        database.Swords.AddRange(CreateDefaultSwords());
        database.Axes.AddRange(CreateDefaultAxes());
        database.Wands.AddRange(CreateDefaultWands());
        database.Staff.AddRange(CreateDefaultStaff());
        return database;
    }

    public static IEnumerable<MonsterProfile> CreateDefaultMonsters() => DbCreateDefault.Database.MonsterProfiles();

    public static IEnumerable<GenericItemEntity> CreateDefaultItems() => DbCreateDefault.Database.Items();

    public static IEnumerable<HelmetEntity> CreateDefaultHelmets() => DbCreateDefault.Database.Helmets();

    public static IEnumerable<GlovesEntity> CreateDefaultGloves() => DbCreateDefault.Database.Gloves();

    public static IEnumerable<ChestEntity> CreateDefaultChests() => DbCreateDefault.Database.Chests();

    public static IEnumerable<LegsEntity> CreateDefaultLegs() => DbCreateDefault.Database.Legs();

    public static IEnumerable<BootsEntity> CreateDefaultBoots() => DbCreateDefault.Database.Boots();

    public static IEnumerable<SwordEntity> CreateDefaultSwords() => DbCreateDefault.Database.Swords();

    public static IEnumerable<AxeEntity> CreateDefaultAxes() => DbCreateDefault.Database.Axes();

    public static IEnumerable<WandEntity> CreateDefaultWands() => DbCreateDefault.Database.Wands();

    public static IEnumerable<StaffEntity> CreateDefaultStaff() => DbCreateDefault.Database.Staff();
}
