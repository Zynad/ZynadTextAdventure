using TextAdventure.Classes;
using TextAdventure.Mechanics;

namespace TextAdventure.Characters;
public class Player : Human
{
    public Dice Dice { get; set; }
    public Vocation Vocation { get; set; }
}
