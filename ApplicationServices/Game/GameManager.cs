namespace ApplicationServices.Game;
public class GameManager : IGameManager
{
    private readonly IStartGame _startGame;

    public GameManager(IStartGame startGame)
    {
        _startGame = startGame;
    }

    public Task StartGame()
    {
        _startGame.Start();
        return Task.CompletedTask;
    }
}
