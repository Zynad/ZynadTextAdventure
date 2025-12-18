using Domain.Entities.Armor.Models;
using Domain.ValueObjects;
using Domain.Entities.Weapons.Models;
using Domain.Enums;

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

    public static IEnumerable<HelmetEntity> CreateDefaultHelmets()
    {
        return new List<HelmetEntity>
        {
            new()
            {
                Name = "Leather Hood",
                LevelRequirement = 1,
                Rarity = RarityEntity.Common,
                Value = 15,
                Weight = 2,
                Durability = 30,
                Material = ArmorMaterialEntity.Leather,
                PhysicalDefense = 3,
                MagicResistance = 1
            }
        };
    }

    public static IEnumerable<GlovesEntity> CreateDefaultGloves()
    {
        return new List<GlovesEntity>
        {
            new()
            {
                Name = "Scout's Wraps",
                LevelRequirement = 1,
                Rarity = RarityEntity.Common,
                Value = 12,
                Weight = 1,
                Durability = 25,
                Material = ArmorMaterialEntity.Leather,
                PhysicalDefense = 2,
                MagicResistance = 1
            }
        };
    }

    public static IEnumerable<ChestEntity> CreateDefaultChests()
    {
        return new List<ChestEntity>
        {
            new()
            {
                Name = "Ranger's Vest",
                LevelRequirement = 2,
                Rarity = RarityEntity.Uncommon,
                Value = 45,
                Weight = 5,
                Durability = 60,
                Material = ArmorMaterialEntity.Leather,
                PhysicalDefense = 8,
                MagicResistance = 3
            }
        };
    }

    public static IEnumerable<LegsEntity> CreateDefaultLegs()
    {
        return new List<LegsEntity>
        {
            new()
            {
                Name = "Hardened Greaves",
                LevelRequirement = 3,
                Rarity = RarityEntity.Uncommon,
                Value = 60,
                Weight = 6,
                Durability = 70,
                Material = ArmorMaterialEntity.Scale,
                PhysicalDefense = 10,
                MagicResistance = 4
            }
        };
    }

    public static IEnumerable<BootsEntity> CreateDefaultBoots()
    {
        return new List<BootsEntity>
        {
            new()
            {
                Name = "Traveler's Boots",
                LevelRequirement = 1,
                Rarity = RarityEntity.Common,
                Value = 18,
                Weight = 2,
                Durability = 35,
                Material = ArmorMaterialEntity.Leather,
                PhysicalDefense = 3,
                MagicResistance = 1
            }
        };
    }

    public static IEnumerable<SwordEntity> CreateDefaultSwords()
    {
        return new List<SwordEntity>
        {
            new()
            {
                Name = "Steel Longsword",
                LevelRequirement = 2,
                Rarity = RarityEntity.Common,
                Value = 70,
                Weight = 7,
                Durability = 65,
                Material = WeaponMaterialEntity.Steel,
                WeaponType = WeaponTypeEntity.Sword,
                MeleeAttackValue = 9,
                RangedAttackValue = 0,
                MagicAttackValue = 0,
                IsRanged = false,
                TwoHanded = false,
                Range = 1,
                MagicPower = 0
            }
        };
    }

    public static IEnumerable<AxeEntity> CreateDefaultAxes()
    {
        return new List<AxeEntity>
        {
            new()
            {
                Name = "Woodcutter's Axe",
                LevelRequirement = 1,
                Rarity = RarityEntity.Common,
                Value = 55,
                Weight = 8,
                Durability = 55,
                Material = WeaponMaterialEntity.Iron,
                WeaponType = WeaponTypeEntity.Axe,
                MeleeAttackValue = 10,
                RangedAttackValue = 0,
                MagicAttackValue = 0,
                IsRanged = false,
                TwoHanded = true,
                Range = 1,
                MagicPower = 0
            }
        };
    }

    public static IEnumerable<WandEntity> CreateDefaultWands()
    {
        return new List<WandEntity>
        {
            new()
            {
                Name = "Apprentice Wand",
                LevelRequirement = 1,
                Rarity = RarityEntity.Common,
                Value = 40,
                Weight = 1,
                Durability = 40,
                Material = WeaponMaterialEntity.Wood,
                WeaponType = WeaponTypeEntity.Wand,
                MeleeAttackValue = 1,
                RangedAttackValue = 0,
                MagicAttackValue = 6,
                IsRanged = true,
                TwoHanded = false,
                Range = 4,
                MagicPower = 8
            }
        };
    }

    public static IEnumerable<StaffEntity> CreateDefaultStaff()
    {
        return new List<StaffEntity>
        {
            new()
            {
                Name = "Elderwood Staff",
                LevelRequirement = 3,
                Rarity = RarityEntity.Uncommon,
                Value = 90,
                Weight = 4,
                Durability = 70,
                Material = WeaponMaterialEntity.Wood,
                WeaponType = WeaponTypeEntity.Staff,
                MeleeAttackValue = 4,
                RangedAttackValue = 0,
                MagicAttackValue = 10,
                IsRanged = true,
                TwoHanded = true,
                Range = 5,
                MagicPower = 12
            }
        };
    }
}
