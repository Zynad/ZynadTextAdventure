using TextAdventure.Items.Equipment;

namespace TextAdventure.Items.Equipment.Weapons;
public class WeaponBase : EquipmentBase
{
    public WeaponMaterial Material { get; set; }
    public WeaponType WeaponType { get; set; }
    public int ArmorValue { get; set; }
    public int MeleeAttackValue { get; set; }
    public int RangedAttackValue { get; set; }
    public int MagicAttackValue { get; set; }
    public bool IsRanged { get; set; }
    public bool TwoHanded { get; set; }
    public int Range { get; set; }
    public int MagicPower { get; set; }
}
