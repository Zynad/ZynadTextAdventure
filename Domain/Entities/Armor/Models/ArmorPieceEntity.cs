using Domain.Entities.Equipment.Models;
using Domain.Enums;

namespace Domain.Entities.Armor.Models;

public class ArmorPieceEntity : EquipmentBaseEntity
{
    public ArmorMaterialEntity Material { get; set; }
    public int ArmorValue { get; set; }
}

