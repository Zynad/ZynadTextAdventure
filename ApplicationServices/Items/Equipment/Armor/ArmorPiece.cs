namespace ApplicationServices.Items.Equipment.Armor;

public abstract class ArmorPiece : EquipmentBase
{
    public ArmorMaterial Material { get; set; }
    public int ArmorValue { get; set; }
}
