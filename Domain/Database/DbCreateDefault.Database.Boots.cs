using Domain.Entities.Armor.Models;
using Domain.Enums;

namespace Domain.Database;

public static partial class DbCreateDefault
{
    public static partial class Database
    {
        public static IEnumerable<BootsEntity> Boots()
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
                    Material = ArmorMaterialEntity.Cloth,
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
                    Material = ArmorMaterialEntity.Cloth,
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
    }
}
