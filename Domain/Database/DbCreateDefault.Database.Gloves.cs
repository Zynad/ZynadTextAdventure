using Domain.Entities.Armor.Models;
using Domain.Enums;

namespace Domain.Database;

public static partial class DbCreateDefault
{
    public static partial class Database
    {
        public static IEnumerable<GlovesEntity> Gloves()
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
                    Material = ArmorMaterialEntity.Cloth,
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
                    Material = ArmorMaterialEntity.Cloth,
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
                    Material = ArmorMaterialEntity.Cloth,
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
    }
}
