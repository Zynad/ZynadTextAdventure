namespace Domain.Entities.Storage;

public class DropTable
{
    public string Biome { get; set; } = string.Empty;

    public List<string> Drops { get; set; } = new();
}
