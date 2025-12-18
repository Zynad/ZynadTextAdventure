namespace Domain.ValueObjects;

public class DropTable
{
    public string Biome { get; set; } = string.Empty;

    public List<string> Drops { get; set; } = [];
}
