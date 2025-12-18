namespace Domain.Core;

public class CharacterStats
{
    public int Combat { get; set; }
        = 1;

    public int Stealth { get; set; }
        = 1;

    public int Pickpocket { get; set; }
        = 1;

    public static CharacterStats Default()
    {
        return new CharacterStats
        {
            Combat = 2,
            Stealth = 2,
            Pickpocket = 2
        };
    }
}
