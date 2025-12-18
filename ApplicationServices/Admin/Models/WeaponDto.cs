using Domain.Enums;

namespace ApplicationServices.Admin.Models;

public record WeaponDto(
    Guid Id,
    string Name,
    int LevelRequirement,
    RarityEntity Rarity,
    int Value,
    int Weight,
    int Durability,
    WeaponMaterialEntity Material,
    WeaponTypeEntity WeaponType,
    int MeleeAttackValue,
    int RangedAttackValue,
    int MagicAttackValue,
    bool IsRanged,
    bool TwoHanded,
    int Range,
    int MagicPower);
