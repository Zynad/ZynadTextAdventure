using ApplicationServices.Admin;

namespace ApplicationServices.Game;
public class GameManager(IStartGame startGame, IAdminManager adminManager) : IGameManager
{
    public async Task StartGame()
    {
        await adminManager.AdminLogin();
        await startGame.Start();
    }
}
