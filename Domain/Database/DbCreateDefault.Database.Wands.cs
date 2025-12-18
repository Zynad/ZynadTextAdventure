using Domain.Entities.Weapons.Models;
using Domain.Enums;

namespace Domain.Database;

public static partial class DbCreateDefault
{
    public static partial class Database
    {
        public static IEnumerable<WandEntity> Wands()
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
                    Material = WeaponMaterialEntity.Silver,
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
                    Material = WeaponMaterialEntity.Silver,
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
                    Material = WeaponMaterialEntity.Silver,
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
                    Material = WeaponMaterialEntity.Silver,
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
    }
}
