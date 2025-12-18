namespace ApplicationServices.Characters.Dto;

public class CreateCharacterRequestDto
{
    public string Name { get; set; } = string.Empty;

    public string PresetId { get; set; } = string.Empty;
}
