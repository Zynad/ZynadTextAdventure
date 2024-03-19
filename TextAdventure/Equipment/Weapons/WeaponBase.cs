namespace TextAdventure.Equipment.Weapons;
public class WeaponBase : EquipmentBase
{
    public WeaponMaterial Material { get; set; }
    public int ArmorValue { get; set; }
    public int Durability { get; set; }
    public int AttackValue { get; set; }
}
