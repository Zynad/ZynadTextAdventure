using TextAdventure.Game.Helpers;
using TextAdventure.Items.Equipment.Armor;
using TextAdventure.Items.Equipment.Weapons;
using TextAdventure.Items.Equipment.Weapons.BaseWeapons;
using TextAdventure.PlayerSettings;

namespace TextAdventure.Classes;
public class Mage : Vocation
{
    public Mage()
    {
        VocationName = "Mage";
    }

    public override void SetBaseValues(Player player)
    {
        var allowedArmor = new List<ArmorMaterial>();
        var allowedWeapon = new List<WeaponType>();
        player.SetBaseValues(30, 10, 5, 50, 30, 15, 10, 30, allowedArmor, allowedWeapon);
    }
    public override void ChooseWeapon(Player player)
    {
        WeaponBase startingWeapon = null;
        while (startingWeapon == null)
        {
            string weaponChoice = ParseHelper.AskForString("Choose your starting weapon:\n1. Staff\n2. Wand\n");
            startingWeapon = CreateWeapon(weaponChoice);
            if (startingWeapon == null)
            {
                Console.WriteLine("Invalid choice");
            }
        }
        player.MainHand = startingWeapon;
        Console.WriteLine($"You have chosen a {player.MainHand.Name} as your starting weapon.");
    }

    public override void ChooseEquipment(Player player)
    {
        throw new NotImplementedException();
    }

    private WeaponBase CreateWeapon(string weaponChoice)
    {
        return weaponChoice switch
        {
            "staff" or "1" => new Staff().BeginnerStaff(),
            "wand" or "2" => new Wand().BeginnerWand(),
            _ => null
        };
    }
}
