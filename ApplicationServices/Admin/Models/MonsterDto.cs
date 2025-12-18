namespace ApplicationServices.Admin.Models;

public record MonsterDto(
    string Name,
    string Description,
    int Level,
    int HitPoints,
    int AttackPower);
