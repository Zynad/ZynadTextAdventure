namespace Domain.Core;

public class TownNpc
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public bool IsVendor { get; set; }
        = false;

    public string? Personality { get; set; }
        = null;
}
