namespace ApplicationServices.Admin.Models;

public record TownDto(
    string Name,
    IReadOnlyCollection<VendorPriceDto> VendorInventory,
    IReadOnlyCollection<TownNpcDto> Npcs);
