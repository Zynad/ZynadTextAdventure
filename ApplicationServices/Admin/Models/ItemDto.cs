using Domain.Enums;

namespace ApplicationServices.Admin.Models;

public record ItemDto(
    Guid Id,
    string Name,
    int LevelRequirement,
    RarityEntity Rarity,
    int Value,
    int Weight);
