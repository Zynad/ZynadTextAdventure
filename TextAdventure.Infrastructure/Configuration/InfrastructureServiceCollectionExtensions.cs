using ApplicationServices.Contracts.Repositories;
using ApplicationServices.Contracts.Services;
using Domain.Database;
using Domain.Repos.Armor;
using Domain.Repos.Weapons;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TextAdventure.Infrastructure.Database;
using TextAdventure.Infrastructure.Repositories;
using TextAdventure.Infrastructure.Repositories.Armor;
using TextAdventure.Infrastructure.Repositories.Weapons;
using TextAdventure.Infrastructure.Services;

namespace TextAdventure.Infrastructure.Configuration;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddTextAdventureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DataStoreOptions>(configuration.GetSection("DataStore"));
        services.Configure<AuthOptions>(configuration.GetSection("Auth"));
        services.Configure<JsonDatabaseOptions>(configuration.GetSection("JsonDatabase"));

        services.AddSingleton<FileConcurrencyProvider>();

        services.AddSingleton<IGameDatabase, JsonDatabase>();
        services.AddSingleton<IUserRepository, JsonUserRepository>();
        services.AddSingleton<ISessionRepository, JsonSessionRepository>();
        services.AddSingleton<ICharacterRepository, JsonCharacterRepository>();
        services.AddSingleton<IQuestRepository, JsonQuestRepository>();
        services.AddSingleton<IWorldRepository, JsonWorldRepository>();
        services.AddSingleton<IHelmetRepository, JsonHelmetRepository>();
        services.AddSingleton<IChestRepository, JsonChestRepository>();
        services.AddSingleton<IGlovesRepository, JsonGlovesRepository>();
        services.AddSingleton<ILegsRepository, JsonLegsRepository>();
        services.AddSingleton<IBootsRepository, JsonBootsRepository>();
        services.AddSingleton<ISwordRepository, JsonSwordRepository>();
        services.AddSingleton<IAxeRepository, JsonAxeRepository>();
        services.AddSingleton<IWandRepository, JsonWandRepository>();
        services.AddSingleton<IStaffRepository, JsonStaffRepository>();

        services.AddSingleton<IRandomService, RandomService>();
        services.AddSingleton<IAuthService, AuthService>();
        services.AddSingleton<IVendorPricingService, VendorPricingService>();

        return services;
    }
}
