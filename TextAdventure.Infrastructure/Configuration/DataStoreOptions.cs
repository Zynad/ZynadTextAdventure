namespace TextAdventure.Infrastructure.Configuration;

public class DataStoreOptions
{
    public string DataDirectory { get; set; } = "Data";
    public string AccountsFileName { get; set; } = "accounts.json";
    public string SessionsFileName { get; set; } = "sessions.json";
    public string CharactersFileName { get; set; } = "characters.json";
    public string QuestsFileName { get; set; } = "quests.json";
    public string WorldFileName { get; set; } = "world.json";
    public string VendorPricingFileName { get; set; } = "vendor-pricing.json";
}
