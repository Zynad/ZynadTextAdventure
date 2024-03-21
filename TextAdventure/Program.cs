using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ApplicationServices.Game;
using ApplicationServices.Services.Weapons.WeaponServices;

namespace TextAdventure
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a new service collection
            var serviceCollection = new ServiceCollection();
            // Configure your services
            ConfigureServices(serviceCollection);
            // Build your service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();
            // Resolve the dependency for your game manager and start the game
            var game = serviceProvider.GetRequiredService<IGameManager>();
            game.StartGame();
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            // Add configuration
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..\\..\\..\\Config"))
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration configuration = builder.Build();
            services.AddSingleton<IConfiguration>(configuration);
            // Add your services here
            services.AddSingleton<IGameManager, GameManager>();
            services.AddSingleton<IStartGame, StartGame>();
            //services.AddScoped<Mage>();
            services.AddTransient<IWandService, WandService>();
        }
    }
}