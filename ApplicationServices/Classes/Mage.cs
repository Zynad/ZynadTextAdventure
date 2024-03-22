using ApplicationServices.Game.Helpers;
using ApplicationServices.Items.Equipment.Armor;
using ApplicationServices.Items.Equipment.Weapons;
using ApplicationServices.PlayerSettings;
using ApplicationServices.Services.Weapons.WeaponServices;

namespace ApplicationServices.Classes;
public class Mage : Vocation
{
    private readonly IWandService _wandService;
    private readonly IStaffService _staffService;
    public Mage(IWandService wandService, IStaffService staffService)
    {
        _wandService = wandService ?? throw new ArgumentNullException(nameof(wandService));
        _staffService = staffService ?? throw new ArgumentNullException(nameof(staffService));
        VocationName = "Mage";
    }

    public override async Task SetBaseValues(Player player)
    {
        var allowedArmor = new List<ArmorMaterial>();
        var allowedWeapon = new List<WeaponType>();
        player.SetBaseValues(30, 10, 5, 50, 30, 15, 10, 30, allowedArmor, allowedWeapon);
        await ChooseWeapon(player);
        await ChooseEquipment(player);
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

    private async Task ChooseEquipment(Player player)
    {
        throw new NotImplementedException();
    }

    private async Task<WeaponBase?> CreateWeapon(string weaponChoice)
    {
        return weaponChoice switch
        {
            "staff" or "1" => await _staffService.GetWeapon(x => x.Name == "Beginner Staff"),
            "wand" or "2" => await _wandService.GetWeapon(x => x.Name == "Beginner Wand"),
            _ => null
        };
    }

}
