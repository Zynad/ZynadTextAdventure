using ApplicationServices.PlayerSettings;

namespace ApplicationServices.Game;
public interface IStartGame
{
    Task<Player> Start();
}
