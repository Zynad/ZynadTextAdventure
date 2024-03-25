using ApplicationServices.Admin;

namespace ApplicationServices.Game;
public class GameManager : IGameManager
{
    private readonly IStartGame _startGame;
    private readonly IAdminManager _adminManager;

    public GameManager(IStartGame startGame, IAdminManager adminManager)
    {
        _startGame = startGame;
        _adminManager = adminManager;
    }

    public async Task StartGame()
    {
        await _adminManager.AdminLogin();
        await _startGame.Start();
    }
}
