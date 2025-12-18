using Domain.Entities.Weapons.Models;
using Domain.Enums;

namespace Domain.Database;

public static partial class DbCreateDefault
{
    public static partial class Database
    {
        public static IEnumerable<StaffEntity> Staff()
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
                    Material = WeaponMaterialEntity.Silver,
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
                    Material = WeaponMaterialEntity.Silver,
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
                    Material = WeaponMaterialEntity.Silver,
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
                    Material = WeaponMaterialEntity.Silver,
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
                    Material = WeaponMaterialEntity.Silver,
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
}
