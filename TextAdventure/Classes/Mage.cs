using TextAdventure.Game.Helpers;
using TextAdventure.Items.Equipment.Armor;
using TextAdventure.Items.Equipment.Weapons;
using TextAdventure.Items.Equipment.Weapons.BaseWeapons;
using TextAdventure.PlayerSettings;

namespace TextAdventure.Classes;
public class Mage : Vocation
{
    private readonly IWeaponService<WeaponBase> _weaponsService;
    public Mage(IWeaponService<WeaponBase> weaponsService)
    {
        _weaponsService = weaponsService ?? throw new ArgumentNullException(nameof(weaponsService));
        VocationName = "Mage";
    }

    public override void SetBaseValues(Player player)
    {
        var allowedArmor = new List<ArmorMaterial>();
        var allowedWeapon = new List<WeaponType>();
        player.SetBaseValues(30, 10, 5, 50, 30, 15, 10, 30, allowedArmor, allowedWeapon);
        ChooseWeapon(player);
        ChooseEquipment(player);
    }
    private async Task ChooseWeapon(Player player)
    {
        WeaponBase startingWeapon = null;
        while (startingWeapon == null)
        {
            string weaponChoice = ParseHelper.AskForString("Choose your starting weapon:\n1. Staff\n2. Wand\n");
            startingWeapon = await CreateWeapon(weaponChoice);
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

    private async Task<WeaponBase> CreateWeapon(string weaponChoice)
    {
        return weaponChoice switch
        {
            "staff" or "1" => await _weaponsService.GetWeapon(x => x.Name == "Beginner Staff" && x is Staff),
            "wand" or "2" => await _weaponsService.GetWeapon(x => x.Name == "Beginner Wand" && x is Wand),
            _ => null
        };
    }

}
