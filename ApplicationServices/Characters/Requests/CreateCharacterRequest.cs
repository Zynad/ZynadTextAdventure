namespace ApplicationServices.Characters.Requests;

public class CreateCharacterRequest
{
    public string Name { get; set; } = string.Empty;

    public string PresetId { get; set; } = string.Empty;
}
