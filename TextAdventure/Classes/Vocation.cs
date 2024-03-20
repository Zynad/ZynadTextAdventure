using TextAdventure.PlayerSettings;

namespace TextAdventure.Classes;
public abstract class Vocation
{
    public abstract void SetBaseValues(Player player);
    public abstract void ChooseWeapon(Player player);
    public abstract void ChooseEquipment(Player player);
    public string VocationName { get; set; }
}
