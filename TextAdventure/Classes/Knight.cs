using TextAdventure.Items.Equipment.Weapons.BaseWeapons;
using TextAdventure.Items.Equipment.Weapons;
using TextAdventure.PlayerSettings;
using TextAdventure.Game.Helpers;
using TextAdventure.Items.Equipment.Armor;

namespace TextAdventure.Classes;
public class Knight : Vocation
{
    public Knight()
    {
        VocationName = "Knight";
    }

    public override void SetBaseValues(Player player)
    {
        var allowedArmor = new List<ArmorMaterial>();
        var allowedWeapon = new List<WeaponType>();
        player.SetBaseValues(60, 20, 20, 10, 60, 30, 20, 5, allowedArmor, allowedWeapon);
        ChooseWeapon(player);
        ChooseEquipment(player);
    }
    private void ChooseWeapon(Player player)
    {
        WeaponBase startingWeapon = null;
        while (startingWeapon == null)
        {
            string weaponChoice = ParseHelper.AskForString("Choose your starting weapon:\n1. Sword\n2. Axe\n");
            startingWeapon = CreateWeapon(weaponChoice);
            if (startingWeapon == null)
            {
                Console.WriteLine("Invalid choice");
            }
        }
        player.MainHand = startingWeapon;
        Console.WriteLine($"You have chosen a {player.MainHand.Name} as your starting weapon.");
    }

    private void ChooseEquipment(Player player)
    {
        throw new NotImplementedException();
    }

    private WeaponBase CreateWeapon(string weaponChoice)
    {
        return weaponChoice switch
        {
            "sword" or "1" => new Sword(),
            "axe" or "2" => new Axe(),
            _ => null
        };
    }
}
