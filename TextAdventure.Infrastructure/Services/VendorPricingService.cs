using ApplicationServices.Contracts.Repositories;
using ApplicationServices.Contracts.Services;
using Domain.Core;
using Domain.ValueObjects;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TextAdventure.Infrastructure.Configuration;
using TextAdventure.Infrastructure.Repositories;

namespace TextAdventure.Infrastructure.Services;

internal class VendorPricingState
{
    public DateTimeOffset ValidUntil { get; set; } = DateTimeOffset.MinValue;
    public List<VendorPriceModifier> Modifiers { get; set; } = [];
}

public class VendorPricingService(
    IWorldRepository worldRepository,
    IRandomService randomService,
    IOptions<DataStoreOptions> options,
    IHostEnvironment environment,
    ILogger<VendorPricingService> logger,
    FileConcurrencyProvider concurrencyProvider)
    : IVendorPricingService
{
    private readonly JsonFileStore<VendorPricingState> _store = new(
        options,
        environment,
        logger,
        concurrencyProvider,
        options.Value.VendorPricingFileName);

    private static readonly TimeSpan RefreshWindow = TimeSpan.FromHours(6);

    public async Task<IReadOnlyCollection<VendorPrice>> GetPricesAsync(string townName, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(townName))
        {
            return [];
        }

        var towns = await worldRepository.GetTownsAsync(cancellationToken);
        var town = towns.FirstOrDefault(t => string.Equals(t.Name, townName, StringComparison.OrdinalIgnoreCase));
        if (town is null)
        {
            return [];
        }

        var state = await ReadStateAsync(cancellationToken);
        if (state.ValidUntil <= DateTimeOffset.UtcNow)
        {
            state = await RefreshAsync(towns, cancellationToken);
        }

        var modifiers = state.Modifiers
            .Where(m => string.Equals(m.TownName, town.Name, StringComparison.OrdinalIgnoreCase))
            .ToList();

        var prices = town.VendorInventory
            .Select(p => ApplyModifier(p, modifiers))
            .ToList();

        return prices;
    }

    public async Task<VendorPrice?> GetPriceForItemAsync(string townName, string itemId, CancellationToken cancellationToken = default)
    {
        var prices = await GetPricesAsync(townName, cancellationToken);
        return prices.FirstOrDefault(p => string.Equals(p.ItemId, itemId, StringComparison.OrdinalIgnoreCase));
    }

    private async Task<VendorPricingState> ReadStateAsync(CancellationToken cancellationToken)
    {
        var states = await _store.ReadAsync(() => [new()], cancellationToken);
        return states.First();
    }

    private async Task<VendorPricingState> RefreshAsync(IEnumerable<Town> towns, CancellationToken cancellationToken)
    {
        var modifiers = new List<VendorPriceModifier>();
        foreach (var town in towns)
        {
            foreach (var price in town.VendorInventory)
            {
                modifiers.Add(new VendorPriceModifier
                {
                    TownName = town.Name,
                    ItemId = price.ItemId,
                    BuyMultiplier = SampleMultiplier(0.9m, 1.2m),
                    SellMultiplier = SampleMultiplier(0.85m, 1.1m),
                    ExpiresAt = DateTimeOffset.UtcNow.Add(RefreshWindow)
                });
            }
        }

        var state = new VendorPricingState
        {
            ValidUntil = DateTimeOffset.UtcNow.Add(RefreshWindow),
            Modifiers = modifiers
        };

        await _store.WriteAsync([state], cancellationToken);
        logger.LogInformation("Vendor pricing refreshed with {ModifierCount} modifiers", modifiers.Count);

        return state;
    }

    private VendorPrice ApplyModifier(VendorPrice basePrice, IReadOnlyCollection<VendorPriceModifier> modifiers)
    {
        var modifier = modifiers.FirstOrDefault(m => string.Equals(m.ItemId, basePrice.ItemId, StringComparison.OrdinalIgnoreCase));
        if (modifier is null)
        {
            return new VendorPrice
            {
                ItemId = basePrice.ItemId,
                BuyPrice = basePrice.BuyPrice,
                SellPrice = basePrice.SellPrice
            };
        }

        return new VendorPrice
        {
            ItemId = basePrice.ItemId,
            BuyPrice = Math.Round(basePrice.BuyPrice * modifier.BuyMultiplier, 2, MidpointRounding.AwayFromZero),
            SellPrice = Math.Round(basePrice.SellPrice * modifier.SellMultiplier, 2, MidpointRounding.AwayFromZero)
        };
    }

    private decimal SampleMultiplier(decimal min, decimal max)
    {
        var range = max - min;
        var roll = (decimal)randomService.NextDouble();
        return min + (range * roll);
    }
}
