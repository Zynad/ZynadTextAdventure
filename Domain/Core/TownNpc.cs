namespace Domain.Core;

public class TownNpc
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public NpcRoleType RoleType { get; set; } = NpcRoleType.Flavor;

    public bool IsVendor { get; set; }
        = false;

    public string? Personality { get; set; }
        = null;

    public string Location { get; set; } = string.Empty;

    public List<string> QuestsOffered { get; set; } = [];

    public NpcDialogueTemplate Dialogue { get; set; } = new();
}
