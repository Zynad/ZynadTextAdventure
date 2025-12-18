namespace ApplicationServices.Admin.Models;

public record DropTableDto(string Biome, IReadOnlyCollection<string> Drops);
