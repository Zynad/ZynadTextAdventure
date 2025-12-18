using Domain.Entities.Armor.Models;
using Domain.Enums;

namespace Domain.Database;

public static partial class DbCreateDefault
{
    public static partial class Database
    {
        public static IEnumerable<LegsEntity> Legs()
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
                    Material = ArmorMaterialEntity.Cloth,
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
                    Material = ArmorMaterialEntity.Cloth,
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
    }
}
