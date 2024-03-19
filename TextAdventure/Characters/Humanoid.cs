using TextAdventure.Equipment.Armor;
using TextAdventure.Equipment.Weapons;

namespace TextAdventure.Characters;
public class Humanoid : Creature
{
    public Helmet Helmet { get; set; }
    public Boots Boots { get; set; }
    public Chest Chest { get; set; }
    public Gloves Gloves { get; set; }
    public Legs Legs { get; set; }
    public WeaponBase MainHand { get; set; }
    public WeaponBase OffHand { get; set; }

    internal void SetArmorAndAttackValue()
    {
        ArmorValue = Boots.ArmorValue + Helmet.ArmorValue + Chest.ArmorValue + Gloves.ArmorValue + Legs.ArmorValue;
        AttackDamage = MainHand.AttackValue + OffHand.AttackValue;
    }
}
