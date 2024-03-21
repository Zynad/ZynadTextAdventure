﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ApplicationServices.Classes;
using ApplicationServices.Game;
using ApplicationServices.Game.Helpers;
using ApplicationServices.Services.Weapons.WeaponServices;
using Domain.Contexts;
using Domain.Repos.Weapons;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
            // Add DbContext
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Database1"))
                    .EnableDetailedErrors());
            // Add your services here
            services.AddSingleton<IGameManager, GameManager>();
            services.AddSingleton<IStartGame, StartGame>();
            services.AddSingleton<IWandRepository, WandRepository>();
            services.AddSingleton<IStaffRepository, StaffRepository>();
            services.AddSingleton<IWandService, WandService>();
            services.AddScoped<Mage>();
            services.AddTransient<DBHandler>();
        }
    }
}