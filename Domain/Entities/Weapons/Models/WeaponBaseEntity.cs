using Domain.Entities.Equipment.Models;
using Domain.Enums;

namespace Domain.Entities.Weapons.Models;
public class WeaponBaseEntity : EquipmentBaseEntity
{
    public WeaponMaterialEntity Material { get; set; }
    public WeaponTypeEntity WeaponType { get; set; }
    public int ArmorValue { get; set; }
    public int MeleeAttackValue { get; set; }
    public int RangedAttackValue { get; set; }
    public int MagicAttackValue { get; set; }
    public bool IsRanged { get; set; }
    public bool TwoHanded { get; set; }
    public int Range { get; set; }
    public int MagicPower { get; set; }
}
