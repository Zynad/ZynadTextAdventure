namespace TextAdventure.Mechanics;
public class Dice
{
    private readonly Random _random = new Random();

    public int RollD4()
    {
        return _random.Next(1, 5);
    }

    public int RollD6()
    {
        return _random.Next(1, 7);
    }
    public int RollD8()
    {
        return _random.Next(1, 9);
    }
    public int RollD10()
    {
        return _random.Next(1, 11);
    }
    public int RollD12()
    {
        return _random.Next(1, 13);
    }
    public int RollD14()
    {
        return _random.Next(1, 15);
    }
    public int RollD16()
    {
        return _random.Next(1, 17);
    }
    public int RollD18()
    {
        return _random.Next(1, 19);
    }
}
