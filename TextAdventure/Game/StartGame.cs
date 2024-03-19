using TextAdventure.Classes;
using TextAdventure.Items.Equipment.Weapons;
using TextAdventure.Items.Equipment.Weapons.BaseWeapons;
using TextAdventure.PlayerSettings;

namespace TextAdventure.Game;
public class StartGame
{
    private Player _player = new Player();
    public Player Start()
    {
        SetNameAndAge();
        ChooseVocation();
        _player.SetBaseValues();
        ChooseStartEquipment();
        return _player;
    }

    private void SetNameAndAge()
    {
        Console.WriteLine("Hello and Welcome");
        Console.Write("Please enter your firstname here : ");
        _player.FirstName = Console.ReadLine();
        Console.Write("Please enter your lastname here : ");
        _player.LastName = Console.ReadLine();
        Console.WriteLine($"Welcome {_player.Name}");
        int age;
        while (true)
        {
            Console.Write($"Now tell me {_player.FirstName}, how old are you? ");
            if (int.TryParse(Console.ReadLine(), out age) && age > 0)
            {
                _player.Age = age;
                break;
            }
            
            Console.WriteLine("That was not a correct number, please try again.");
            
        }
    }

    private void ChooseVocation()
    {
        while (true)
        {
            int choice;
            Console.WriteLine("Choose your class");
            Console.WriteLine("1. Knight");
            Console.WriteLine("2. Mage");
            while (!int.TryParse(Console.ReadLine(), out choice) || (choice != 1 && choice != 2))
            {
                Console.WriteLine("Invalid choice");
                Console.WriteLine("Choose your class");
                Console.WriteLine("1. Knight");
                Console.WriteLine("2. Mage");
            }

            var vocation = choice switch
            {
                1 => new Knight(),
                2 => new Mage(),
                _ => _player.Vocation
            };
            Console.WriteLine($"So you choose to be a {vocation.VocationName}");
            Console.WriteLine("Is that correct?");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");
            int.TryParse(Console.ReadLine(), out choice);
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Great choice!");
                    _player.Vocation = vocation;
                    break;
                case 2:
                    Console.WriteLine("Let's choose again!");
                    continue;
                default:
                    Console.WriteLine("Invalid choice");
                    Console.WriteLine("Choose your class");
                    Console.WriteLine("1. Knight");
                    Console.WriteLine("2. Mage");
                    continue;
            }
        }
    }

    private void ChooseStartEquipment()
    {
        switch (_player.Vocation)
        {
            case Mage: 
                ChooseMageWeapon();
                break;
            case Knight:
                ChooseKnightWeapon();
                break;
        }
        throw new NotImplementedException();
    }

    private void ChooseMageWeapon()
    {
        Console.WriteLine("Choose your starting weapon:");
        Console.WriteLine("1. Staff");
        Console.WriteLine("2. Wand");
        int weaponChoice;
        while (!int.TryParse(Console.ReadLine(), out weaponChoice) || (weaponChoice != 1 && weaponChoice != 2))
        {
            Console.WriteLine("Invalid choice");
            Console.WriteLine("Choose your starting weapon:");
            Console.WriteLine("1. Staff");
            Console.WriteLine("2. Wand");
        }
        WeaponBase startingWeapon = weaponChoice switch
        {
            1 => new Staff(),
            2 => new Wand(),
            _ => null
        };
        if (startingWeapon != null)
        {
            _player.MainHand = startingWeapon;
            
            switch (startingWeapon)
            {
                case Staff staff: 
                    staff.BeginnerStaff();
                    break;
                case Wand wand:
                    wand.BeginnerWand();
                    break;
            }
            Console.WriteLine($"You have chosen a {_player.MainHand.Name} as your starting weapon.");
        }
        else
        {
            Console.WriteLine("Invalid choice");
        }
    }

    private void ChooseKnightWeapon()
    {
        Console.WriteLine("Choose your starting weapon:");
        Console.WriteLine("1. Sword");
        Console.WriteLine("2. Axe");
        int weaponChoice;
        while (!int.TryParse(Console.ReadLine(), out weaponChoice) || (weaponChoice != 1 && weaponChoice != 2))
        {
            Console.WriteLine("Invalid choice");
            Console.WriteLine("Choose your starting weapon:");
            Console.WriteLine("1. Sword");
            Console.WriteLine("2. Axe");
        }
        WeaponBase startingWeapon = weaponChoice switch
        {
            1 => new Sword(),
            2 => new Axe(),
            _ => null
        };
        if (startingWeapon != null)
        {
            _player.MainHand = startingWeapon;
            Console.WriteLine($"You have chosen a {_player.MainHand.Name} as your starting weapon.");
        }
        else
        {
            Console.WriteLine("Invalid choice");
        }
    }
}
