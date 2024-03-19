using TextAdventure.Characters;
using TextAdventure.Classes;
using TextAdventure.Mechanics;

namespace TextAdventure.PlayerSettings;
public class Player : Human
{
    public Dice Dice { get; set; } = new Dice();
    public Vocation Vocation { get; set; }

    public void SetBaseValues()
    {
        this.HitPoints = 100;
        this.AttackDamage = 10;
        this.DefenseValue = 5;
    }
}
