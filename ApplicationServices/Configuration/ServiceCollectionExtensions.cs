using ApplicationServices.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationServices.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTextAdventureGame(this IServiceCollection services)
    {
        services.AddSingleton<IGameDataService, GameDataService>();
        services.AddTransient<Authentication.RegisterUserHandler>();
        services.AddTransient<Authentication.LoginUserHandler>();
        services.AddTransient<Authentication.GetCurrentUserHandler>();
        services.AddSingleton<Adventure.EncounterGenerator>();
        services.AddTransient<Characters.GetCharacterPresetsHandler>();
        services.AddTransient<Characters.CreateCharacterHandler>();
        services.AddTransient<Characters.GetCharactersHandler>();
        services.AddTransient<Characters.GetCharacterDetailsHandler>();
        services.AddTransient<Adventure.TravelToLocationHandler>();
        services.AddTransient<Adventure.AcceptQuestHandler>();
        services.AddTransient<Adventure.CompleteQuestHandler>();
        services.AddTransient<Adventure.GetEncountersHandler>();
        services.AddTransient<Npc.NpcInteractionService>();
        return services;
    }
}
