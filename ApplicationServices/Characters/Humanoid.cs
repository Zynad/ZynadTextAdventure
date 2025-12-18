using ApplicationServices.Items.Equipment;
using ApplicationServices.Items.Equipment.Armor.BaseArmors;
using ApplicationServices.Items.Equipment.Weapons;

namespace ApplicationServices.Characters;
public class Humanoid : Creature
{
    public Helmet Helmet { get; set; } = default!;
    public Boots Boots { get; set; } = default!;
    public Chest Chest { get; set; } = default!;
    public Gloves Gloves { get; set; } = default!;
    public Legs Legs { get; set; } = default!;
    public WeaponBase MainHand { get; set; } = default!;
    public EquipmentBase OffHand { get; set; } = default!;

    internal void SetArmorValue()
    {
        ArmorValue = Boots.ArmorValue + Helmet.ArmorValue + Chest.ArmorValue + Gloves.ArmorValue + Legs.ArmorValue + MainHand.ArmorValue;
    }
}
