using Domain.Core;

namespace ApplicationServices.Admin.Models;

public record TownNpcDto(
    string Id,
    string Name,
    string Role,
    NpcRoleType RoleType,
    bool IsVendor,
    string? Personality,
    string Location,
    IReadOnlyCollection<string> QuestsOffered,
    NpcDialogueDto Dialogue);
