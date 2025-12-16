using ApplicationServices.Contracts.Repositories;
using ApplicationServices.Contracts.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TextAdventure.Infrastructure.Repositories;
using TextAdventure.Infrastructure.Services;

namespace TextAdventure.Infrastructure.Configuration;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddTextAdventureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DataStoreOptions>(configuration.GetSection("DataStore"));
        services.Configure<AuthOptions>(configuration.GetSection("Auth"));

        services.AddSingleton<FileConcurrencyProvider>();

        services.AddSingleton<IUserRepository, JsonUserRepository>();
        services.AddSingleton<ISessionRepository, JsonSessionRepository>();
        services.AddSingleton<ICharacterRepository, JsonCharacterRepository>();
        services.AddSingleton<IQuestRepository, JsonQuestRepository>();
        services.AddSingleton<IWorldRepository, JsonWorldRepository>();

        services.AddSingleton<IRandomService, RandomService>();
        services.AddSingleton<IAuthService, AuthService>();
        services.AddSingleton<IVendorPricingService, VendorPricingService>();

        return services;
    }
}
