namespace TextAdventure.Equipment.Armor;

public abstract class ArmorPiece : EquipmentBase
{
    public ArmorMaterial Material { get; set; }
    public int ArmorValue { get; set; }
    public int Durability { get; set; }
}
