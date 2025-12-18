using Domain.Entities.Armor.Models;
using Domain.Enums;

namespace Domain.Database;

public static partial class DbCreateDefault
{
    public static partial class Database
    {
        public static IEnumerable<HelmetEntity> Helmets()
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
                    Material = ArmorMaterialEntity.Cloth,
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
                    Material = ArmorMaterialEntity.Cloth,
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
                    Material = ArmorMaterialEntity.Cloth,
                    PhysicalDefense = 6,
                    MagicResistance = 14
                }
            };
        }
    }
}
