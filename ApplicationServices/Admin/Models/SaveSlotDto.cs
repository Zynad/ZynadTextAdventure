namespace ApplicationServices.Admin.Models;

public record SaveSlotDto(
    Guid Id,
    string Name,
    int Level,
    int Experience,
    string AdventureState,
    DateTimeOffset LastUpdatedUtc,
    WorldLocationDto Location);
