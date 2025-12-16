namespace Domain.ValueObjects;

public class VendorPrice
{
    public string ItemId { get; set; } = string.Empty;
    public decimal BuyPrice { get; set; }
    public decimal SellPrice { get; set; }
}
