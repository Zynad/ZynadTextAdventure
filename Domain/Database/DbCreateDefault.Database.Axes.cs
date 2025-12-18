using Domain.Entities.Weapons.Models;
using Domain.Enums;

namespace Domain.Database;

public static partial class DbCreateDefault
{
    public static partial class Database
    {
        public static IEnumerable<AxeEntity> Axes()
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
                    Material = WeaponMaterialEntity.Copper,
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
                    Material = WeaponMaterialEntity.Silver,
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
                    Material = WeaponMaterialEntity.Silver,
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
                    Material = WeaponMaterialEntity.Silver,
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
                    Material = WeaponMaterialEntity.Silver,
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
                    Material = WeaponMaterialEntity.Silver,
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
    }
}
