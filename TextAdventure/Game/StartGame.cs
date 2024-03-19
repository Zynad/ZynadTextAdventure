using System.Numerics;
using TextAdventure.Characters;
using TextAdventure.Classes;

namespace TextAdventure.Game;
public class StartGame
{
    public Player Start()
    {
        var player = new Player();
        Console.WriteLine("Hello and Welcome");
        Console.Write("Please enter your firstname here : ");
        player.FirstName = Console.ReadLine();
        Console.Write("Please enter your lastname here : ");
        player.LastName = Console.ReadLine();
        Console.WriteLine($"Welcome {player.Name}");
        player.Vocation = ChooseClass(player);
        return player;
    }

    private Vocation ChooseClass(Player player)
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

            Vocation vocation = choice switch
            {
                1 => new Knight(),
                2 => new Mage(),
                _ => player.Vocation
            };
            Console.WriteLine($"So you choose to be a {vocation.VocationName}");
            Console.WriteLine("Is that correct?");
            Console.Write("1. Yes");
            Console.Write("2. No");
            int.TryParse(Console.ReadLine(), out choice);
            if (choice == 1)
            {
                Console.WriteLine("Great choice!");
            }
            else if (choice == 2)
            {
                Console.WriteLine("Let's choose again!");
                continue;
            }
            else
            {
                Console.WriteLine("Invalid choice");
                Console.WriteLine("Choose your class");
                Console.WriteLine("1. Knight");
                Console.WriteLine("2. Mage");
                continue;
            }
            return vocation;
        }
    }
}
