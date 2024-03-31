using ApplicationServices.Characters;
using ApplicationServices.Classes;
using ApplicationServices.Game.Helpers;
using ApplicationServices.PlayerSettings;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationServices.Game;
public class StartGame : IStartGame
{
    private Player _player = new();
    private readonly IServiceProvider _serviceProvider;

    public StartGame(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public async Task<Player> Start()
    {
        await SetPersonalInfo();
        await ChooseVocation();
        await StartStory();
        return _player;
    }

    private async Task SetPersonalInfo()
    {
        Console.WriteLine("Game Master : Hello and Welcome to Zynadria");
        _player.Gender = ParseHelper.AskForEnum<Gender>("Game Master : Are you Male or Female? : ");
        _player.FirstName = ParseHelper.AskForName("Game Master : What is your first name? : ");
        _player.LastName = ParseHelper.AskForName("Game Master : And your last name? : ");
        Console.WriteLine($"Game Master : Welcome {_player.Name}");
        _player.Age = ParseHelper.AskForInt($"Game Master : Now tell me {_player.FirstName}, how old are you? ");
    }

    private async Task ChooseVocation()
    {
        while (true)
        {
            string choice = ParseHelper.AskForString("Game Master : Choose your class:\n1. Knight\n2. Mage\n");
            var vocation = choice switch
            {
                "knight" or "1" => _serviceProvider.GetRequiredService<Knight>(),
                "mage" or "2" => _serviceProvider.GetRequiredService<Mage>(),
                _ => _player.Vocation
            };
            Console.WriteLine($"Game Master : So you choose to be a {vocation.VocationName}");
            string confirmChoice = ParseHelper.AskForString("Game Master : Are you sure of that? It is a very permanent choice\n1. Yes\n2. No\n");
            switch (confirmChoice)
            {
                case "yes" or "1":
                    Console.WriteLine("Game Master : Great choice!");
                    _player.Vocation = vocation;
                    await SetStartProperties();
                    return;
                case "no" or "2":
                    Console.WriteLine("Game Master : Let's choose again!");
                    continue;
                default:
                    Console.WriteLine("Game Master : Invalid choice");
                    continue;
            }
        }
    }

    private async Task SetStartProperties()
    {
        await _player.Vocation.SetBaseValues(_player);
    }

    private async Task StartStory()
    {
        string choice = ParseHelper.AskForString("Game Master : Do you want me to describe the world to you?\n1. No\n2. Yes\n");
        switch (choice)
        {
            case "yes" or "1":
                Console.WriteLine("Game Master : Let me tell you about the world of Zynadria...");
                // Add code to describe the world
                break;
            case "no" or "2":
                Console.WriteLine("Game Master : Alright, let's continue...");
                // Add code to continue the game without describing the world
                break;
            default:
                Console.WriteLine("Game Master : Invalid choice");
                // Add code to handle invalid choice
                break;
        }
    }
}

