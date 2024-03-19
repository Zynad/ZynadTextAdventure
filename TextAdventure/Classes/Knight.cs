using TextAdventure.Items.Equipment.Weapons.BaseWeapons;
using TextAdventure.Items.Equipment.Weapons;
using TextAdventure.PlayerSettings;
using TextAdventure.Game.Helpers;

namespace TextAdventure.Classes;
public class Knight : Vocation
{
    public Knight()
    {
        VocationName = "Knight";
    }

    public override void SetBaseValues(Player player)
    {
        player.SetBaseValues(60, 20, 20, 10, 60, 30, 20, 5);
    }
    public override void ChooseWeapon(Player player)
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
