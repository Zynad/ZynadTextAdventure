using TextAdventure.Items.Equipment.Weapons;
using TextAdventure.Repos.Equipment.Models;

namespace TextAdventure.Repos.Weapons.Models;
public class WeaponBaseEntity : EquipmentBaseEntity
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
