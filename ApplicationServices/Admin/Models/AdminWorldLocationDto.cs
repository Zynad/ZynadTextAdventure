namespace ApplicationServices.Admin.Models;

public record AdminWorldLocationDto(
    string Id,
    string Name,
    string Description,
    string Biome,
    string ThreatLevel,
    IReadOnlyCollection<string> AdjacentLocationIds,
    string? TownName);
