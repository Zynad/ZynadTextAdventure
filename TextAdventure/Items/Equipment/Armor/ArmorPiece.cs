using TextAdventure.Equipment.Armor;

namespace TextAdventure.Items.Equipment.Armor;

public abstract class ArmorPiece : EquipmentBase
{
    public ArmorMaterial Material { get; set; }
    public int ArmorValue { get; set; }
    public int Durability { get; set; }
}
