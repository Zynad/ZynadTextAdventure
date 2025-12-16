using ApplicationServices.Contracts.Repositories;
using ApplicationServices.Contracts.Services;
using Domain.ValueObjects;

namespace TextAdventure.Infrastructure.Services;

public class VendorPricingService : IVendorPricingService
{
    private readonly IWorldRepository _worldRepository;

    public VendorPricingService(IWorldRepository worldRepository)
    {
        _worldRepository = worldRepository;
    }

    public async Task<IReadOnlyCollection<VendorPrice>> GetPricesAsync(string townName, CancellationToken cancellationToken = default)
    {
        var towns = await _worldRepository.GetTownsAsync(cancellationToken);
        var town = towns.FirstOrDefault(t => string.Equals(t.Name, townName, StringComparison.OrdinalIgnoreCase));
        return town?.VendorInventory ?? Array.Empty<VendorPrice>();
    }

    public async Task<VendorPrice?> GetPriceForItemAsync(string townName, string itemId, CancellationToken cancellationToken = default)
    {
        var prices = await GetPricesAsync(townName, cancellationToken);
        return prices.FirstOrDefault(p => string.Equals(p.ItemId, itemId, StringComparison.OrdinalIgnoreCase));
    }
}
