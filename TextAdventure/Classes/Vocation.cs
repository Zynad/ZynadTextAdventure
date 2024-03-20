using TextAdventure.PlayerSettings;

namespace TextAdventure.Classes;
public abstract class Vocation
{
    public abstract void SetBaseValues(Player player);
    public string VocationName { get; set; }
}
