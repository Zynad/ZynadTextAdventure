namespace TextAdventure.Game;
public class GameManager
{
    private readonly StartGame _startGame = new StartGame();

    public void StartGame()
    {
        _startGame.Start();
    }
}
