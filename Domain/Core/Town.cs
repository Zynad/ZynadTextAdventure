using Domain.ValueObjects;

namespace Domain.Core;

public class Town
{
    public string Name { get; set; } = string.Empty;
    public List<VendorPrice> VendorInventory { get; set; } = new();
}
