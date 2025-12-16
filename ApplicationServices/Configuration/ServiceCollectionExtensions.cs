using ApplicationServices.Services;
using Domain.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationServices.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTextAdventureGame(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JsonDatabaseOptions>(configuration.GetSection("JsonDatabase"));
        services.AddSingleton<IGameDatabase, JsonDatabase>();
        services.AddSingleton<IGameDataService, GameDataService>();
        services.AddTransient<Authentication.RegisterUserHandler>();
        services.AddTransient<Authentication.LoginUserHandler>();
        services.AddTransient<Authentication.GetCurrentUserHandler>();
        services.AddTransient<Characters.GetCharacterPresetsHandler>();
        services.AddTransient<Characters.CreateCharacterHandler>();
        services.AddTransient<Characters.GetCharactersHandler>();
        services.AddTransient<Characters.GetCharacterDetailsHandler>();
        return services;
    }
}
