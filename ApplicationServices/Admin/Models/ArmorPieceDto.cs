using Domain.Enums;

namespace ApplicationServices.Admin.Models;

public record ArmorPieceDto(
    Guid Id,
    string Name,
    int LevelRequirement,
    RarityEntity Rarity,
    int Value,
    int Weight,
    int Durability,
    ArmorMaterialEntity Material,
    int PhysicalDefense,
    int MagicResistance,
    ArmorSlot Slot);
