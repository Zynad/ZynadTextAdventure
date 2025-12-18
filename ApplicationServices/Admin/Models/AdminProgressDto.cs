namespace ApplicationServices.Admin.Models;

public record AdminProgressDto(
    Guid UserId,
    int Level,
    int Experience,
    string AdventureState,
    DateTimeOffset LastUpdatedUtc,
    IReadOnlyCollection<SaveSlotDto> SaveSlots);
