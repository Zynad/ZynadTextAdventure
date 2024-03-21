using ApplicationServices.Characters;
using ApplicationServices.Classes;
using ApplicationServices.Game.Helpers;
using ApplicationServices.PlayerSettings;
using ApplicationServices.Services.Weapons.WeaponServices;
using Domain.Entities.Weapons.Models;
using Domain.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationServices.Game;
public class StartGame : IStartGame
{
    private Player _player = new Player();
    private readonly IServiceProvider _serviceProvider;
    private readonly IWandService _wandService;

    public StartGame(IServiceProvider serviceProvider, IWandService wandService)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _wandService = wandService;
    }

    public async Task<Player> Start()
    {
        SetPersonalInfo();
        ChooseVocation();
        return _player;
    }

    private void SetPersonalInfo()
    {
        Console.WriteLine("Hello and Welcome to Zynadria");
        _player.Gender = ParseHelper.AskForEnum<Gender>("Are you Male or Female? : ");
        _player.FirstName = ParseHelper.AskForName("Please enter your firstname here : ");
        _player.LastName = ParseHelper.AskForName("Please enter your lastname here : ");
        Console.WriteLine($"Welcome {_player.Name}");
        _player.Age = ParseHelper.AskForInt($"Now tell me {_player.FirstName}, how old are you? ");
    }

    private void ChooseVocation()
    {
        while (true)
        {
            string choice = ParseHelper.AskForString("Choose your class:\n1. Knight\n2. Mage\n");
            var vocation = choice switch
            {
                "knight" or "1" => new Knight(),
                "mage" or "2" => _serviceProvider.GetRequiredService<Mage>(),
                _ => _player.Vocation
            };
            Console.WriteLine($"So you choose to be a {vocation.VocationName}");
            string confirmChoice = ParseHelper.AskForString("Is that correct?\n1. Yes\n2. No\n");
            switch (confirmChoice)
            {
                case "yes" or "1":
                    Console.WriteLine("Great choice!");
                    _player.Vocation = vocation;
                    SetStartProperties();
                    return;
                case "no" or "2":
                    Console.WriteLine("Let's choose again!");
                    continue;
                default:
                    Console.WriteLine("Invalid choice");
                    continue;
            }
        }
    }

    private async Task SetStartProperties()
    {
        await _player.Vocation.SetBaseValues(_player);
    }
}
