using Domain.Entities.Weapons.Models;
using Domain.Enums;

namespace Domain.Database;

public static partial class DbCreateDefault
{
    public static partial class Database
    {
        public static IEnumerable<SwordEntity> Swords()
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
                    Material = WeaponMaterialEntity.Silver,
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
                    Material = WeaponMaterialEntity.Silver,
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
                    Material = WeaponMaterialEntity.Silver,
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
                    Material = WeaponMaterialEntity.Silver,
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
    }
}
