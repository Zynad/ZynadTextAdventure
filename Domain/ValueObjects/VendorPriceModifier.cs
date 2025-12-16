namespace Domain.ValueObjects;

public class VendorPriceModifier
{
    public string TownName { get; set; } = string.Empty;
    public string ItemId { get; set; } = string.Empty;
    public decimal BuyMultiplier { get; set; } = 1m;
    public decimal SellMultiplier { get; set; } = 1m;
    public DateTimeOffset ExpiresAt { get; set; } = DateTimeOffset.UtcNow;
}
