using TextAdventure.Items.Equipment.Armor;
using TextAdventure.Repos.Equipment.Models;

namespace TextAdventure.Repos.Armor.Models;

public class ArmorPieceEntity : EquipmentBaseEntity
{
    public ArmorMaterial Material { get; set; }
    public int ArmorValue { get; set; }
}

