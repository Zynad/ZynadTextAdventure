using ApplicationServices.Items.Equipment;
using ApplicationServices.Items.Equipment.Armor.BaseArmors;
using ApplicationServices.Items.Equipment.Weapons;

namespace ApplicationServices.Characters;
public class Humanoid : Creature
{
    public Helmet Helmet { get; set; }
    public Boots Boots { get; set; }
    public Chest Chest { get; set; }
    public Gloves Gloves { get; set; }
    public Legs Legs { get; set; }
    public WeaponBase? MainHand { get; set; }
    public EquipmentBase OffHand { get; set; }

    internal void SetArmorValue()
    {
        ArmorValue = Boots.ArmorValue + Helmet.ArmorValue + Chest.ArmorValue + Gloves.ArmorValue + Legs.ArmorValue;
    }
}
