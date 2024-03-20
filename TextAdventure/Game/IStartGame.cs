using TextAdventure.PlayerSettings;

namespace TextAdventure.Game;
public interface IStartGame
{
    Task<Player> Start();
}
