using ApplicationServices.Services;
using ApplicationServices.Admin;
using Microsoft.Extensions.DependencyInjection;
using Domain.Repos.Armor;
using Domain.Repos.Weapons;
using Domain.Repos.Items;

namespace ApplicationServices.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTextAdventureGame(this IServiceCollection services)
    {
        services.AddSingleton<IGameDataService, GameDataService>();
        services.AddSingleton<IAdminArmorService, AdminArmorService>();
        services.AddSingleton<IAdminWeaponService, AdminWeaponService>();
        services.AddSingleton<IAdminItemService, AdminItemService>();
        services.AddSingleton<IAdminMonsterService, AdminMonsterService>();

        services.AddSingleton<IHelmetRepository, HelmetRepository>();
        services.AddSingleton<IChestRepository, ChestRepository>();
        services.AddSingleton<IGlovesRepository, GlovesRepository>();
        services.AddSingleton<ILegsRepository, LegsRepository>();
        services.AddSingleton<IBootsRepository, BootsRepository>();
        services.AddSingleton<IWandRepository, WandRepository>();
        services.AddSingleton<IStaffRepository, StaffRepository>();
        services.AddSingleton<ISwordRepository, SwordRepository>();
        services.AddSingleton<IAxeRepository, AxeRepository>();
        services.AddSingleton<IItemRepository, ItemRepository>();
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
