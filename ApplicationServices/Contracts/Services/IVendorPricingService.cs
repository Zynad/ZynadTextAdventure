using Domain.ValueObjects;

namespace ApplicationServices.Contracts.Services;

public interface IVendorPricingService
{
    Task<IReadOnlyCollection<VendorPrice>> GetPricesAsync(string townName, CancellationToken cancellationToken = default);
    Task<VendorPrice?> GetPriceForItemAsync(string townName, string itemId, CancellationToken cancellationToken = default);
}
