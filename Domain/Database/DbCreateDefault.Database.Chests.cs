using Domain.Entities.Armor.Models;
using Domain.Enums;

namespace Domain.Database;

public static partial class DbCreateDefault
{
    public static partial class Database
    {
        public static IEnumerable<ChestEntity> Chests()
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
                    Material = ArmorMaterialEntity.Cloth,
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
                    Material = ArmorMaterialEntity.Cloth,
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
                    Material = ArmorMaterialEntity.Cloth,
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
    }
}
