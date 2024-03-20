using TextAdventure.Characters;
using TextAdventure.Classes;
using TextAdventure.Game.Helpers;
using TextAdventure.Items.Equipment.Weapons;
using TextAdventure.PlayerSettings;

namespace TextAdventure.Game;
public class StartGame : IStartGame
{
    private Player _player = new Player();
    private readonly IWeaponService<WeaponBase> _weaponsService;
    public StartGame(IWeaponService<WeaponBase> weaponsService)
    {
        _weaponsService = weaponsService ?? throw new ArgumentNullException(nameof(weaponsService));
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
                "mage" or "2" => new Mage(_weaponsService),
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

    private void SetStartProperties()
    {
        _player.Vocation.SetBaseValues(_player);
    }
}
