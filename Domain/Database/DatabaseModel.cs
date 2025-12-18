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
            },
            new()
            {
                Name = "Bronze Visor",
                LevelRequirement = 2,
                Rarity = RarityEntity.Common,
                Value = 28,
                Weight = 3,
                Durability = 40,
                Material = ArmorMaterialEntity.Plate,
                PhysicalDefense = 5,
                MagicResistance = 1
            },
            new()
            {
                Name = "Scout's Cap",
                LevelRequirement = 3,
                Rarity = RarityEntity.Uncommon,
                Value = 42,
                Weight = 2,
                Durability = 55,
                Material = ArmorMaterialEntity.Leather,
                PhysicalDefense = 6,
                MagicResistance = 2
            },
            new()
            {
                Name = "Plated Greathelm",
                LevelRequirement = 5,
                Rarity = RarityEntity.Rare,
                Value = 90,
                Weight = 6,
                Durability = 90,
                Material = ArmorMaterialEntity.Plate,
                PhysicalDefense = 12,
                MagicResistance = 4
            },
            new()
            {
                Name = "Mystic Circlet",
                LevelRequirement = 4,
                Rarity = RarityEntity.Uncommon,
                Value = 75,
                Weight = 1,
                Durability = 50,
                Material = ArmorMaterialEntity.Silk,
                PhysicalDefense = 2,
                MagicResistance = 7
            },
            new()
            {
                Name = "Hunter's Coif",
                LevelRequirement = 2,
                Rarity = RarityEntity.Common,
                Value = 26,
                Weight = 2,
                Durability = 38,
                Material = ArmorMaterialEntity.Leather,
                PhysicalDefense = 4,
                MagicResistance = 2
            },
            new()
            {
                Name = "Scale Helm",
                LevelRequirement = 4,
                Rarity = RarityEntity.Uncommon,
                Value = 68,
                Weight = 4,
                Durability = 70,
                Material = ArmorMaterialEntity.Scale,
                PhysicalDefense = 10,
                MagicResistance = 3
            },
            new()
            {
                Name = "Iron Guard Helmet",
                LevelRequirement = 3,
                Rarity = RarityEntity.Common,
                Value = 50,
                Weight = 5,
                Durability = 65,
                Material = ArmorMaterialEntity.Plate,
                PhysicalDefense = 8,
                MagicResistance = 2
            },
            new()
            {
                Name = "Battlemage Hood",
                LevelRequirement = 5,
                Rarity = RarityEntity.Rare,
                Value = 105,
                Weight = 2,
                Durability = 80,
                Material = ArmorMaterialEntity.Silk,
                PhysicalDefense = 4,
                MagicResistance = 10
            },
            new()
            {
                Name = "Gladiator Helm",
                LevelRequirement = 6,
                Rarity = RarityEntity.Rare,
                Value = 130,
                Weight = 6,
                Durability = 95,
                Material = ArmorMaterialEntity.Plate,
                PhysicalDefense = 14,
                MagicResistance = 4
            },
            new()
            {
                Name = "Frostbound Hood",
                LevelRequirement = 7,
                Rarity = RarityEntity.Epic,
                Value = 160,
                Weight = 3,
                Durability = 100,
                Material = ArmorMaterialEntity.Silk,
                PhysicalDefense = 6,
                MagicResistance = 14
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
            },
            new()
            {
                Name = "Leather Gloves",
                LevelRequirement = 2,
                Rarity = RarityEntity.Common,
                Value = 18,
                Weight = 1,
                Durability = 32,
                Material = ArmorMaterialEntity.Leather,
                PhysicalDefense = 3,
                MagicResistance = 1
            },
            new()
            {
                Name = "Archer's Grips",
                LevelRequirement = 3,
                Rarity = RarityEntity.Uncommon,
                Value = 32,
                Weight = 1,
                Durability = 40,
                Material = ArmorMaterialEntity.Leather,
                PhysicalDefense = 4,
                MagicResistance = 2
            },
            new()
            {
                Name = "Battlemage Wraps",
                LevelRequirement = 4,
                Rarity = RarityEntity.Uncommon,
                Value = 44,
                Weight = 1,
                Durability = 45,
                Material = ArmorMaterialEntity.Silk,
                PhysicalDefense = 2,
                MagicResistance = 6
            },
            new()
            {
                Name = "Steel Gauntlets",
                LevelRequirement = 4,
                Rarity = RarityEntity.Uncommon,
                Value = 58,
                Weight = 3,
                Durability = 70,
                Material = ArmorMaterialEntity.Plate,
                PhysicalDefense = 9,
                MagicResistance = 1
            },
            new()
            {
                Name = "Scale Graspers",
                LevelRequirement = 5,
                Rarity = RarityEntity.Rare,
                Value = 74,
                Weight = 2,
                Durability = 75,
                Material = ArmorMaterialEntity.Scale,
                PhysicalDefense = 10,
                MagicResistance = 3
            },
            new()
            {
                Name = "Enchanter's Gloves",
                LevelRequirement = 5,
                Rarity = RarityEntity.Rare,
                Value = 80,
                Weight = 1,
                Durability = 60,
                Material = ArmorMaterialEntity.Silk,
                PhysicalDefense = 3,
                MagicResistance = 9
            },
            new()
            {
                Name = "Guardian Gauntlets",
                LevelRequirement = 6,
                Rarity = RarityEntity.Rare,
                Value = 96,
                Weight = 3,
                Durability = 85,
                Material = ArmorMaterialEntity.Plate,
                PhysicalDefense = 12,
                MagicResistance = 2
            },
            new()
            {
                Name = "Assassin's Handwraps",
                LevelRequirement = 6,
                Rarity = RarityEntity.Rare,
                Value = 88,
                Weight = 1,
                Durability = 70,
                Material = ArmorMaterialEntity.Leather,
                PhysicalDefense = 6,
                MagicResistance = 4
            },
            new()
            {
                Name = "Frostwoven Mitts",
                LevelRequirement = 7,
                Rarity = RarityEntity.Epic,
                Value = 120,
                Weight = 1,
                Durability = 90,
                Material = ArmorMaterialEntity.Silk,
                PhysicalDefense = 4,
                MagicResistance = 12
            },
            new()
            {
                Name = "Dragonscale Gauntlets",
                LevelRequirement = 8,
                Rarity = RarityEntity.Epic,
                Value = 150,
                Weight = 3,
                Durability = 110,
                Material = ArmorMaterialEntity.Scale,
                PhysicalDefense = 15,
                MagicResistance = 5
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
            },
            new()
            {
                Name = "Padded Tunic",
                LevelRequirement = 1,
                Rarity = RarityEntity.Common,
                Value = 20,
                Weight = 3,
                Durability = 35,
                Material = ArmorMaterialEntity.Leather,
                PhysicalDefense = 5,
                MagicResistance = 1
            },
            new()
            {
                Name = "Bronze Cuirass",
                LevelRequirement = 3,
                Rarity = RarityEntity.Common,
                Value = 55,
                Weight = 8,
                Durability = 70,
                Material = ArmorMaterialEntity.Plate,
                PhysicalDefense = 12,
                MagicResistance = 2
            },
            new()
            {
                Name = "Scale Mail",
                LevelRequirement = 4,
                Rarity = RarityEntity.Uncommon,
                Value = 78,
                Weight = 7,
                Durability = 85,
                Material = ArmorMaterialEntity.Scale,
                PhysicalDefense = 14,
                MagicResistance = 3
            },
            new()
            {
                Name = "Mystic Robes",
                LevelRequirement = 4,
                Rarity = RarityEntity.Uncommon,
                Value = 72,
                Weight = 2,
                Durability = 55,
                Material = ArmorMaterialEntity.Silk,
                PhysicalDefense = 4,
                MagicResistance = 10
            },
            new()
            {
                Name = "Guardian Plate",
                LevelRequirement = 5,
                Rarity = RarityEntity.Rare,
                Value = 110,
                Weight = 10,
                Durability = 110,
                Material = ArmorMaterialEntity.Plate,
                PhysicalDefense = 18,
                MagicResistance = 4
            },
            new()
            {
                Name = "Shadow Jerkin",
                LevelRequirement = 5,
                Rarity = RarityEntity.Rare,
                Value = 98,
                Weight = 4,
                Durability = 80,
                Material = ArmorMaterialEntity.Leather,
                PhysicalDefense = 10,
                MagicResistance = 5
            },
            new()
            {
                Name = "Arcanist Vestments",
                LevelRequirement = 6,
                Rarity = RarityEntity.Rare,
                Value = 130,
                Weight = 3,
                Durability = 90,
                Material = ArmorMaterialEntity.Silk,
                PhysicalDefense = 6,
                MagicResistance = 14
            },
            new()
            {
                Name = "Dragonscale Aegis",
                LevelRequirement = 7,
                Rarity = RarityEntity.Epic,
                Value = 170,
                Weight = 9,
                Durability = 130,
                Material = ArmorMaterialEntity.Scale,
                PhysicalDefense = 20,
                MagicResistance = 7
            },
            new()
            {
                Name = "Frostbound Raiment",
                LevelRequirement = 7,
                Rarity = RarityEntity.Epic,
                Value = 165,
                Weight = 3,
                Durability = 115,
                Material = ArmorMaterialEntity.Silk,
                PhysicalDefense = 8,
                MagicResistance = 16
            },
            new()
            {
                Name = "Champion Breastplate",
                LevelRequirement = 8,
                Rarity = RarityEntity.Epic,
                Value = 190,
                Weight = 11,
                Durability = 140,
                Material = ArmorMaterialEntity.Plate,
                PhysicalDefense = 24,
                MagicResistance = 6
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
            },
            new()
            {
                Name = "Traveler's Leggings",
                LevelRequirement = 1,
                Rarity = RarityEntity.Common,
                Value = 18,
                Weight = 2,
                Durability = 30,
                Material = ArmorMaterialEntity.Leather,
                PhysicalDefense = 3,
                MagicResistance = 1
            },
            new()
            {
                Name = "Bronze Greaves",
                LevelRequirement = 2,
                Rarity = RarityEntity.Common,
                Value = 32,
                Weight = 5,
                Durability = 50,
                Material = ArmorMaterialEntity.Plate,
                PhysicalDefense = 7,
                MagicResistance = 1
            },
            new()
            {
                Name = "Ranger's Trousers",
                LevelRequirement = 2,
                Rarity = RarityEntity.Common,
                Value = 28,
                Weight = 3,
                Durability = 40,
                Material = ArmorMaterialEntity.Leather,
                PhysicalDefense = 5,
                MagicResistance = 2
            },
            new()
            {
                Name = "Scale Legguards",
                LevelRequirement = 4,
                Rarity = RarityEntity.Uncommon,
                Value = 64,
                Weight = 6,
                Durability = 78,
                Material = ArmorMaterialEntity.Scale,
                PhysicalDefense = 12,
                MagicResistance = 3
            },
            new()
            {
                Name = "Mystic Legwraps",
                LevelRequirement = 4,
                Rarity = RarityEntity.Uncommon,
                Value = 58,
                Weight = 2,
                Durability = 55,
                Material = ArmorMaterialEntity.Silk,
                PhysicalDefense = 4,
                MagicResistance = 9
            },
            new()
            {
                Name = "Guardian Greaves",
                LevelRequirement = 5,
                Rarity = RarityEntity.Rare,
                Value = 92,
                Weight = 7,
                Durability = 100,
                Material = ArmorMaterialEntity.Plate,
                PhysicalDefense = 16,
                MagicResistance = 3
            },
            new()
            {
                Name = "Shadow Leggings",
                LevelRequirement = 5,
                Rarity = RarityEntity.Rare,
                Value = 86,
                Weight = 3,
                Durability = 85,
                Material = ArmorMaterialEntity.Leather,
                PhysicalDefense = 9,
                MagicResistance = 5
            },
            new()
            {
                Name = "Frostbound Greaves",
                LevelRequirement = 7,
                Rarity = RarityEntity.Epic,
                Value = 140,
                Weight = 5,
                Durability = 120,
                Material = ArmorMaterialEntity.Scale,
                PhysicalDefense = 18,
                MagicResistance = 7
            },
            new()
            {
                Name = "Arcanist Leggings",
                LevelRequirement = 6,
                Rarity = RarityEntity.Rare,
                Value = 110,
                Weight = 2,
                Durability = 95,
                Material = ArmorMaterialEntity.Silk,
                PhysicalDefense = 6,
                MagicResistance = 13
            },
            new()
            {
                Name = "Champion Legplates",
                LevelRequirement = 8,
                Rarity = RarityEntity.Epic,
                Value = 170,
                Weight = 8,
                Durability = 140,
                Material = ArmorMaterialEntity.Plate,
                PhysicalDefense = 22,
                MagicResistance = 5
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
            },
            new()
            {
                Name = "Scout's Boots",
                LevelRequirement = 2,
                Rarity = RarityEntity.Common,
                Value = 26,
                Weight = 2,
                Durability = 42,
                Material = ArmorMaterialEntity.Leather,
                PhysicalDefense = 4,
                MagicResistance = 2
            },
            new()
            {
                Name = "Bronze Sabatons",
                LevelRequirement = 3,
                Rarity = RarityEntity.Common,
                Value = 40,
                Weight = 4,
                Durability = 60,
                Material = ArmorMaterialEntity.Plate,
                PhysicalDefense = 7,
                MagicResistance = 1
            },
            new()
            {
                Name = "Scale Treads",
                LevelRequirement = 4,
                Rarity = RarityEntity.Uncommon,
                Value = 62,
                Weight = 4,
                Durability = 75,
                Material = ArmorMaterialEntity.Scale,
                PhysicalDefense = 10,
                MagicResistance = 3
            },
            new()
            {
                Name = "Mystic Sandals",
                LevelRequirement = 3,
                Rarity = RarityEntity.Uncommon,
                Value = 48,
                Weight = 1,
                Durability = 50,
                Material = ArmorMaterialEntity.Silk,
                PhysicalDefense = 2,
                MagicResistance = 8
            },
            new()
            {
                Name = "Guardian Greaves",
                LevelRequirement = 5,
                Rarity = RarityEntity.Rare,
                Value = 88,
                Weight = 5,
                Durability = 95,
                Material = ArmorMaterialEntity.Plate,
                PhysicalDefense = 14,
                MagicResistance = 3
            },
            new()
            {
                Name = "Shadowfoot Boots",
                LevelRequirement = 5,
                Rarity = RarityEntity.Rare,
                Value = 82,
                Weight = 2,
                Durability = 85,
                Material = ArmorMaterialEntity.Leather,
                PhysicalDefense = 8,
                MagicResistance = 5
            },
            new()
            {
                Name = "Frostbound Boots",
                LevelRequirement = 7,
                Rarity = RarityEntity.Epic,
                Value = 120,
                Weight = 3,
                Durability = 110,
                Material = ArmorMaterialEntity.Scale,
                PhysicalDefense = 16,
                MagicResistance = 7
            },
            new()
            {
                Name = "Arcanist Slippers",
                LevelRequirement = 6,
                Rarity = RarityEntity.Rare,
                Value = 105,
                Weight = 1,
                Durability = 95,
                Material = ArmorMaterialEntity.Silk,
                PhysicalDefense = 4,
                MagicResistance = 12
            },
            new()
            {
                Name = "Champion Sabatons",
                LevelRequirement = 8,
                Rarity = RarityEntity.Epic,
                Value = 150,
                Weight = 5,
                Durability = 130,
                Material = ArmorMaterialEntity.Plate,
                PhysicalDefense = 20,
                MagicResistance = 4
            },
            new()
            {
                Name = "Ranger's Striders",
                LevelRequirement = 4,
                Rarity = RarityEntity.Uncommon,
                Value = 58,
                Weight = 2,
                Durability = 72,
                Material = ArmorMaterialEntity.Leather,
                PhysicalDefense = 9,
                MagicResistance = 3
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
            },
            new()
            {
                Name = "Iron Shortsword",
                LevelRequirement = 1,
                Rarity = RarityEntity.Common,
                Value = 45,
                Weight = 5,
                Durability = 50,
                Material = WeaponMaterialEntity.Iron,
                WeaponType = WeaponTypeEntity.Sword,
                MeleeAttackValue = 6,
                RangedAttackValue = 0,
                MagicAttackValue = 0,
                IsRanged = false,
                TwoHanded = false,
                Range = 1,
                MagicPower = 0
            },
            new()
            {
                Name = "Knight's Blade",
                LevelRequirement = 3,
                Rarity = RarityEntity.Uncommon,
                Value = 90,
                Weight = 8,
                Durability = 80,
                Material = WeaponMaterialEntity.Steel,
                WeaponType = WeaponTypeEntity.Sword,
                MeleeAttackValue = 12,
                RangedAttackValue = 0,
                MagicAttackValue = 0,
                IsRanged = false,
                TwoHanded = false,
                Range = 1,
                MagicPower = 0
            },
            new()
            {
                Name = "Greatsword of Valor",
                LevelRequirement = 5,
                Rarity = RarityEntity.Rare,
                Value = 140,
                Weight = 11,
                Durability = 110,
                Material = WeaponMaterialEntity.Steel,
                WeaponType = WeaponTypeEntity.Sword,
                MeleeAttackValue = 18,
                RangedAttackValue = 0,
                MagicAttackValue = 0,
                IsRanged = false,
                TwoHanded = true,
                Range = 1,
                MagicPower = 0
            },
            new()
            {
                Name = "Flamebrand",
                LevelRequirement = 5,
                Rarity = RarityEntity.Rare,
                Value = 155,
                Weight = 9,
                Durability = 100,
                Material = WeaponMaterialEntity.Steel,
                WeaponType = WeaponTypeEntity.Sword,
                MeleeAttackValue = 16,
                RangedAttackValue = 0,
                MagicAttackValue = 6,
                IsRanged = false,
                TwoHanded = false,
                Range = 1,
                MagicPower = 8
            },
            new()
            {
                Name = "Silver Rapier",
                LevelRequirement = 4,
                Rarity = RarityEntity.Uncommon,
                Value = 110,
                Weight = 4,
                Durability = 75,
                Material = WeaponMaterialEntity.Silver,
                WeaponType = WeaponTypeEntity.Sword,
                MeleeAttackValue = 13,
                RangedAttackValue = 0,
                MagicAttackValue = 3,
                IsRanged = false,
                TwoHanded = false,
                Range = 1,
                MagicPower = 5
            },
            new()
            {
                Name = "Moonlit Saber",
                LevelRequirement = 6,
                Rarity = RarityEntity.Rare,
                Value = 170,
                Weight = 6,
                Durability = 120,
                Material = WeaponMaterialEntity.Mithril,
                WeaponType = WeaponTypeEntity.Sword,
                MeleeAttackValue = 19,
                RangedAttackValue = 0,
                MagicAttackValue = 7,
                IsRanged = false,
                TwoHanded = false,
                Range = 1,
                MagicPower = 9
            },
            new()
            {
                Name = "Frostbite Edge",
                LevelRequirement = 7,
                Rarity = RarityEntity.Epic,
                Value = 210,
                Weight = 7,
                Durability = 140,
                Material = WeaponMaterialEntity.Mithril,
                WeaponType = WeaponTypeEntity.Sword,
                MeleeAttackValue = 21,
                RangedAttackValue = 0,
                MagicAttackValue = 10,
                IsRanged = false,
                TwoHanded = false,
                Range = 1,
                MagicPower = 12
            },
            new()
            {
                Name = "Stormblade",
                LevelRequirement = 6,
                Rarity = RarityEntity.Rare,
                Value = 180,
                Weight = 8,
                Durability = 125,
                Material = WeaponMaterialEntity.Mithril,
                WeaponType = WeaponTypeEntity.Sword,
                MeleeAttackValue = 20,
                RangedAttackValue = 0,
                MagicAttackValue = 8,
                IsRanged = false,
                TwoHanded = true,
                Range = 1,
                MagicPower = 11
            },
            new()
            {
                Name = "Shadowfang",
                LevelRequirement = 5,
                Rarity = RarityEntity.Rare,
                Value = 150,
                Weight = 5,
                Durability = 105,
                Material = WeaponMaterialEntity.Silver,
                WeaponType = WeaponTypeEntity.Sword,
                MeleeAttackValue = 17,
                RangedAttackValue = 0,
                MagicAttackValue = 5,
                IsRanged = false,
                TwoHanded = false,
                Range = 1,
                MagicPower = 7
            },
            new()
            {
                Name = "Dragonsbreath Claymore",
                LevelRequirement = 8,
                Rarity = RarityEntity.Epic,
                Value = 240,
                Weight = 12,
                Durability = 160,
                Material = WeaponMaterialEntity.Mithril,
                WeaponType = WeaponTypeEntity.Sword,
                MeleeAttackValue = 26,
                RangedAttackValue = 0,
                MagicAttackValue = 12,
                IsRanged = false,
                TwoHanded = true,
                Range = 1,
                MagicPower = 14
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
            },
            new()
            {
                Name = "Bronze Hatchet",
                LevelRequirement = 1,
                Rarity = RarityEntity.Common,
                Value = 35,
                Weight = 6,
                Durability = 45,
                Material = WeaponMaterialEntity.Bronze,
                WeaponType = WeaponTypeEntity.Axe,
                MeleeAttackValue = 8,
                RangedAttackValue = 0,
                MagicAttackValue = 0,
                IsRanged = false,
                TwoHanded = false,
                Range = 1,
                MagicPower = 0
            },
            new()
            {
                Name = "Steel War Axe",
                LevelRequirement = 3,
                Rarity = RarityEntity.Uncommon,
                Value = 85,
                Weight = 9,
                Durability = 80,
                Material = WeaponMaterialEntity.Steel,
                WeaponType = WeaponTypeEntity.Axe,
                MeleeAttackValue = 14,
                RangedAttackValue = 0,
                MagicAttackValue = 0,
                IsRanged = false,
                TwoHanded = true,
                Range = 1,
                MagicPower = 0
            },
            new()
            {
                Name = "Guardian Battleaxe",
                LevelRequirement = 4,
                Rarity = RarityEntity.Uncommon,
                Value = 110,
                Weight = 10,
                Durability = 90,
                Material = WeaponMaterialEntity.Steel,
                WeaponType = WeaponTypeEntity.Axe,
                MeleeAttackValue = 16,
                RangedAttackValue = 0,
                MagicAttackValue = 0,
                IsRanged = false,
                TwoHanded = true,
                Range = 1,
                MagicPower = 0
            },
            new()
            {
                Name = "Berserker Cleaver",
                LevelRequirement = 5,
                Rarity = RarityEntity.Rare,
                Value = 150,
                Weight = 12,
                Durability = 120,
                Material = WeaponMaterialEntity.Steel,
                WeaponType = WeaponTypeEntity.Axe,
                MeleeAttackValue = 20,
                RangedAttackValue = 0,
                MagicAttackValue = 0,
                IsRanged = false,
                TwoHanded = true,
                Range = 1,
                MagicPower = 0
            },
            new()
            {
                Name = "Frostbite Axe",
                LevelRequirement = 6,
                Rarity = RarityEntity.Rare,
                Value = 175,
                Weight = 11,
                Durability = 125,
                Material = WeaponMaterialEntity.Mithril,
                WeaponType = WeaponTypeEntity.Axe,
                MeleeAttackValue = 19,
                RangedAttackValue = 0,
                MagicAttackValue = 8,
                IsRanged = false,
                TwoHanded = false,
                Range = 1,
                MagicPower = 10
            },
            new()
            {
                Name = "Stormchaser Axe",
                LevelRequirement = 6,
                Rarity = RarityEntity.Rare,
                Value = 185,
                Weight = 10,
                Durability = 130,
                Material = WeaponMaterialEntity.Mithril,
                WeaponType = WeaponTypeEntity.Axe,
                MeleeAttackValue = 21,
                RangedAttackValue = 0,
                MagicAttackValue = 6,
                IsRanged = false,
                TwoHanded = true,
                Range = 1,
                MagicPower = 9
            },
            new()
            {
                Name = "Runic Splitter",
                LevelRequirement = 5,
                Rarity = RarityEntity.Rare,
                Value = 165,
                Weight = 9,
                Durability = 115,
                Material = WeaponMaterialEntity.Silver,
                WeaponType = WeaponTypeEntity.Axe,
                MeleeAttackValue = 18,
                RangedAttackValue = 0,
                MagicAttackValue = 7,
                IsRanged = false,
                TwoHanded = false,
                Range = 1,
                MagicPower = 9
            },
            new()
            {
                Name = "Dragonscale Axe",
                LevelRequirement = 7,
                Rarity = RarityEntity.Epic,
                Value = 220,
                Weight = 12,
                Durability = 150,
                Material = WeaponMaterialEntity.Mithril,
                WeaponType = WeaponTypeEntity.Axe,
                MeleeAttackValue = 24,
                RangedAttackValue = 0,
                MagicAttackValue = 9,
                IsRanged = false,
                TwoHanded = true,
                Range = 1,
                MagicPower = 11
            },
            new()
            {
                Name = "Inferno Chopper",
                LevelRequirement = 8,
                Rarity = RarityEntity.Epic,
                Value = 245,
                Weight = 13,
                Durability = 170,
                Material = WeaponMaterialEntity.Mithril,
                WeaponType = WeaponTypeEntity.Axe,
                MeleeAttackValue = 26,
                RangedAttackValue = 0,
                MagicAttackValue = 12,
                IsRanged = false,
                TwoHanded = true,
                Range = 1,
                MagicPower = 14
            },
            new()
            {
                Name = "Celestial Beheader",
                LevelRequirement = 9,
                Rarity = RarityEntity.Legendary,
                Value = 300,
                Weight = 12,
                Durability = 190,
                Material = WeaponMaterialEntity.Mithril,
                WeaponType = WeaponTypeEntity.Axe,
                MeleeAttackValue = 30,
                RangedAttackValue = 0,
                MagicAttackValue = 15,
                IsRanged = false,
                TwoHanded = true,
                Range = 1,
                MagicPower = 16
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
            },
            new()
            {
                Name = "Amber Focus",
                LevelRequirement = 2,
                Rarity = RarityEntity.Common,
                Value = 55,
                Weight = 1,
                Durability = 45,
                Material = WeaponMaterialEntity.Wood,
                WeaponType = WeaponTypeEntity.Wand,
                MeleeAttackValue = 2,
                RangedAttackValue = 0,
                MagicAttackValue = 8,
                IsRanged = true,
                TwoHanded = false,
                Range = 4,
                MagicPower = 10
            },
            new()
            {
                Name = "Sparkling Twig",
                LevelRequirement = 1,
                Rarity = RarityEntity.Common,
                Value = 32,
                Weight = 1,
                Durability = 35,
                Material = WeaponMaterialEntity.Wood,
                WeaponType = WeaponTypeEntity.Wand,
                MeleeAttackValue = 1,
                RangedAttackValue = 0,
                MagicAttackValue = 5,
                IsRanged = true,
                TwoHanded = false,
                Range = 3,
                MagicPower = 7
            },
            new()
            {
                Name = "Scholarly Rod",
                LevelRequirement = 3,
                Rarity = RarityEntity.Uncommon,
                Value = 78,
                Weight = 2,
                Durability = 60,
                Material = WeaponMaterialEntity.Wood,
                WeaponType = WeaponTypeEntity.Wand,
                MeleeAttackValue = 2,
                RangedAttackValue = 0,
                MagicAttackValue = 10,
                IsRanged = true,
                TwoHanded = false,
                Range = 4,
                MagicPower = 12
            },
            new()
            {
                Name = "Crystal Channel",
                LevelRequirement = 4,
                Rarity = RarityEntity.Uncommon,
                Value = 95,
                Weight = 2,
                Durability = 70,
                Material = WeaponMaterialEntity.Silver,
                WeaponType = WeaponTypeEntity.Wand,
                MeleeAttackValue = 3,
                RangedAttackValue = 0,
                MagicAttackValue = 12,
                IsRanged = true,
                TwoHanded = false,
                Range = 5,
                MagicPower = 14
            },
            new()
            {
                Name = "Stormspark Wand",
                LevelRequirement = 5,
                Rarity = RarityEntity.Rare,
                Value = 130,
                Weight = 2,
                Durability = 80,
                Material = WeaponMaterialEntity.Silver,
                WeaponType = WeaponTypeEntity.Wand,
                MeleeAttackValue = 3,
                RangedAttackValue = 0,
                MagicAttackValue = 14,
                IsRanged = true,
                TwoHanded = false,
                Range = 5,
                MagicPower = 16
            },
            new()
            {
                Name = "Frostfire Wand",
                LevelRequirement = 6,
                Rarity = RarityEntity.Rare,
                Value = 150,
                Weight = 2,
                Durability = 90,
                Material = WeaponMaterialEntity.Silver,
                WeaponType = WeaponTypeEntity.Wand,
                MeleeAttackValue = 4,
                RangedAttackValue = 0,
                MagicAttackValue = 15,
                IsRanged = true,
                TwoHanded = false,
                Range = 5,
                MagicPower = 18
            },
            new()
            {
                Name = "Runebound Wand",
                LevelRequirement = 6,
                Rarity = RarityEntity.Rare,
                Value = 165,
                Weight = 2,
                Durability = 95,
                Material = WeaponMaterialEntity.Mithril,
                WeaponType = WeaponTypeEntity.Wand,
                MeleeAttackValue = 4,
                RangedAttackValue = 0,
                MagicAttackValue = 17,
                IsRanged = true,
                TwoHanded = false,
                Range = 5,
                MagicPower = 20
            },
            new()
            {
                Name = "Starcaller's Wand",
                LevelRequirement = 7,
                Rarity = RarityEntity.Epic,
                Value = 190,
                Weight = 3,
                Durability = 110,
                Material = WeaponMaterialEntity.Mithril,
                WeaponType = WeaponTypeEntity.Wand,
                MeleeAttackValue = 5,
                RangedAttackValue = 0,
                MagicAttackValue = 20,
                IsRanged = true,
                TwoHanded = false,
                Range = 6,
                MagicPower = 23
            },
            new()
            {
                Name = "Soulbinder Wand",
                LevelRequirement = 8,
                Rarity = RarityEntity.Epic,
                Value = 220,
                Weight = 3,
                Durability = 120,
                Material = WeaponMaterialEntity.Mithril,
                WeaponType = WeaponTypeEntity.Wand,
                MeleeAttackValue = 6,
                RangedAttackValue = 0,
                MagicAttackValue = 22,
                IsRanged = true,
                TwoHanded = false,
                Range = 6,
                MagicPower = 26
            },
            new()
            {
                Name = "Celestial Conduit",
                LevelRequirement = 9,
                Rarity = RarityEntity.Legendary,
                Value = 260,
                Weight = 3,
                Durability = 135,
                Material = WeaponMaterialEntity.Mithril,
                WeaponType = WeaponTypeEntity.Wand,
                MeleeAttackValue = 6,
                RangedAttackValue = 0,
                MagicAttackValue = 24,
                IsRanged = true,
                TwoHanded = false,
                Range = 7,
                MagicPower = 30
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
            },
            new()
            {
                Name = "Oak Quarterstaff",
                LevelRequirement = 1,
                Rarity = RarityEntity.Common,
                Value = 35,
                Weight = 3,
                Durability = 40,
                Material = WeaponMaterialEntity.Wood,
                WeaponType = WeaponTypeEntity.Staff,
                MeleeAttackValue = 3,
                RangedAttackValue = 0,
                MagicAttackValue = 5,
                IsRanged = true,
                TwoHanded = true,
                Range = 3,
                MagicPower = 6
            },
            new()
            {
                Name = "Initiate's Staff",
                LevelRequirement = 2,
                Rarity = RarityEntity.Common,
                Value = 55,
                Weight = 3,
                Durability = 55,
                Material = WeaponMaterialEntity.Wood,
                WeaponType = WeaponTypeEntity.Staff,
                MeleeAttackValue = 3,
                RangedAttackValue = 0,
                MagicAttackValue = 7,
                IsRanged = true,
                TwoHanded = true,
                Range = 4,
                MagicPower = 8
            },
            new()
            {
                Name = "Runed Staff",
                LevelRequirement = 4,
                Rarity = RarityEntity.Uncommon,
                Value = 105,
                Weight = 4,
                Durability = 80,
                Material = WeaponMaterialEntity.Silver,
                WeaponType = WeaponTypeEntity.Staff,
                MeleeAttackValue = 4,
                RangedAttackValue = 0,
                MagicAttackValue = 12,
                IsRanged = true,
                TwoHanded = true,
                Range = 5,
                MagicPower = 14
            },
            new()
            {
                Name = "Stormcaller Staff",
                LevelRequirement = 5,
                Rarity = RarityEntity.Rare,
                Value = 135,
                Weight = 4,
                Durability = 95,
                Material = WeaponMaterialEntity.Silver,
                WeaponType = WeaponTypeEntity.Staff,
                MeleeAttackValue = 5,
                RangedAttackValue = 0,
                MagicAttackValue = 14,
                IsRanged = true,
                TwoHanded = true,
                Range = 6,
                MagicPower = 16
            },
            new()
            {
                Name = "Frostweaver Staff",
                LevelRequirement = 6,
                Rarity = RarityEntity.Rare,
                Value = 165,
                Weight = 4,
                Durability = 110,
                Material = WeaponMaterialEntity.Mithril,
                WeaponType = WeaponTypeEntity.Staff,
                MeleeAttackValue = 6,
                RangedAttackValue = 0,
                MagicAttackValue = 16,
                IsRanged = true,
                TwoHanded = true,
                Range = 6,
                MagicPower = 19
            },
            new()
            {
                Name = "Emberheart Staff",
                LevelRequirement = 6,
                Rarity = RarityEntity.Rare,
                Value = 180,
                Weight = 4,
                Durability = 115,
                Material = WeaponMaterialEntity.Mithril,
                WeaponType = WeaponTypeEntity.Staff,
                MeleeAttackValue = 6,
                RangedAttackValue = 0,
                MagicAttackValue = 17,
                IsRanged = true,
                TwoHanded = true,
                Range = 6,
                MagicPower = 20
            },
            new()
            {
                Name = "Runebound Greatstaff",
                LevelRequirement = 7,
                Rarity = RarityEntity.Epic,
                Value = 210,
                Weight = 5,
                Durability = 130,
                Material = WeaponMaterialEntity.Mithril,
                WeaponType = WeaponTypeEntity.Staff,
                MeleeAttackValue = 7,
                RangedAttackValue = 0,
                MagicAttackValue = 20,
                IsRanged = true,
                TwoHanded = true,
                Range = 7,
                MagicPower = 24
            },
            new()
            {
                Name = "Starfall Staff",
                LevelRequirement = 8,
                Rarity = RarityEntity.Epic,
                Value = 235,
                Weight = 5,
                Durability = 145,
                Material = WeaponMaterialEntity.Mithril,
                WeaponType = WeaponTypeEntity.Staff,
                MeleeAttackValue = 8,
                RangedAttackValue = 0,
                MagicAttackValue = 22,
                IsRanged = true,
                TwoHanded = true,
                Range = 7,
                MagicPower = 27
            },
            new()
            {
                Name = "Soulshaper Staff",
                LevelRequirement = 9,
                Rarity = RarityEntity.Legendary,
                Value = 270,
                Weight = 5,
                Durability = 165,
                Material = WeaponMaterialEntity.Mithril,
                WeaponType = WeaponTypeEntity.Staff,
                MeleeAttackValue = 9,
                RangedAttackValue = 0,
                MagicAttackValue = 25,
                IsRanged = true,
                TwoHanded = true,
                Range = 8,
                MagicPower = 32
            }
        };
    }
}
