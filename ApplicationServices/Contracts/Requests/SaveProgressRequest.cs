namespace ApplicationServices.Contracts.Requests;

public class SaveProgressRequest
{
    public string Token { get; set; } = string.Empty;

    public int Level { get; set; }

    public int Experience { get; set; }

    public string AdventureState { get; set; } = string.Empty;

    public string SaveSlotName { get; set; } = "Slot 1";

    public string LocationName { get; set; } = string.Empty;

    public string LocationBiome { get; set; } = string.Empty;

    public string LocationThreatLevel { get; set; } = string.Empty;
}
