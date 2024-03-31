using ApplicationServices.Admin;
using ApplicationServices.Classes;
using ApplicationServices.Game;
using ApplicationServices.Game.Helpers;
using ApplicationServices.Items.Equipment.Weapons.Factories;
using ApplicationServices.Services.Armor;
using ApplicationServices.Services.Weapons.WeaponServices;
using Domain.Contexts;
using Domain.Repos.Armor;
using Domain.Repos.Weapons;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ApplicationServices.Items.Equipment.Armor.Factories;

namespace TextAdventure;

class Program
{
    static async Task Main(string[] args)
    {
        // Create a new service collection
        var serviceCollection = new ServiceCollection();
        // Configure your services
        ConfigureServices(serviceCollection);
        // Build your service provider
        var serviceProvider = serviceCollection.BuildServiceProvider();
        // Resolve the dependency for your game manager and start the game
        var game = serviceProvider.GetRequiredService<IGameManager>();
        await game.StartGame();
    }
    private static void ConfigureServices(IServiceCollection services)
    {
        // Add configuration
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..\\..\\..\\Config"))
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        IConfiguration configuration = builder.Build();
        services.AddSingleton<IConfiguration>(configuration);
        // Add DbContext
        services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("LaptopDB"))
           // options.UseSqlServer(configuration.GetConnectionString("Database1"))
                .EnableDetailedErrors());
        
        // Add your services here
        services.AddSingleton<IGameManager, GameManager>();
        services.AddSingleton<IAdminManager, AdminManager>();
        services.AddSingleton<IDbHandler, DbHandler>();
        services.AddSingleton<IStartGame, StartGame>();
        
        // Repositories
        services.AddSingleton<IWandRepository, WandRepository>();
        services.AddSingleton<IStaffRepository, StaffRepository>();
        services.AddSingleton<ISwordRepository, SwordRepository>();
        services.AddSingleton<IAxeRepository, AxeRepository>();
        services.AddSingleton<IBootsRepository, BootsRepository>();
        services.AddSingleton<IChestRepository, ChestRepository>();
        services.AddSingleton<IGlovesRepository, GlovesRepository>();
        services.AddSingleton<IHelmetRepository, HelmetRepository>();
        services.AddSingleton<ILegsRepository, LegsRepository>();
        
        //Services
        services.AddSingleton<IWandService, WandService>();
        services.AddSingleton<IStaffService, StaffService>();
        services.AddSingleton<ISwordService, SwordService>();
        services.AddSingleton<IAxeService, AxeService>();
        services.AddSingleton<IBootsService, BootsService>();
        services.AddSingleton<IChestService, ChestService>();
        services.AddSingleton<IGlovesService, GlovesService>();
        services.AddSingleton<IHelmetService, HelmetService>();
        services.AddSingleton<ILegsService, LegsService>();

        // Factories
        services.AddScoped<IWandFactory, WandFactory>();
        services.AddScoped<IStaffFactory, StaffFactory>();
        services.AddScoped<IAxeFactory, AxeFactory>();
        services.AddScoped<ISwordFactory, SwordFactory>();
        services.AddScoped<IBootsFactory, BootsFactory>();
        services.AddScoped<IChestFactory, ChestFactory>();
        services.AddScoped<ILegsFactory, LegsFactory>();
        services.AddScoped<IGlovesFactory, GlovesFactory>();
        services.AddScoped<IHelmetFactory, HelmetFactory>();
        
        // Classes
        services.AddScoped<Mage>();
        services.AddScoped<Knight>();
        
    }
}
