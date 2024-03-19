using TextAdventure.Game;

namespace TextAdventure;

internal class Program
{
    static void Main(string[] args)
    {
        var game = new GameManager();
        game.StartGame();
    }
}
