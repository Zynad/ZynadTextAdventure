namespace TextAdventure.Game;
public static class RandomGenerator
{
    private static readonly Random _random = new Random();

    public static int RandomNumber(int number1, int number2)
    {
        return _random.Next(number1, number2);
    }
}
