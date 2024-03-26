using ApplicationServices.Game.Helpers;
using ApplicationServices.Items.Equipment.Armor;
using ApplicationServices.Items.Equipment.Weapons;
using ApplicationServices.Items.Equipment.Weapons.BaseWeapons;
using ApplicationServices.PlayerSettings;
using ApplicationServices.Services.Weapons.WeaponServices;

namespace ApplicationServices.Classes;
public class Knight : Vocation
{
    private readonly ISwordService _swordService;
    private readonly IAxeService _axeService;
    public Knight(IAxeService axeService, ISwordService swordService)
    {
        _axeService = axeService ?? throw new ArgumentNullException(nameof(axeService));
        _swordService = swordService ?? throw new ArgumentNullException(nameof(swordService));
        VocationName = "Knight";
    }

    public override async Task SetBaseValues(Player player)
    {
        var allowedArmor = new List<ArmorMaterial>();
        var allowedWeapon = new List<WeaponType>();
        player.SetBaseValues(60, 20, 20, 10, 60, 30, 20, 5, allowedArmor, allowedWeapon);
        await ChooseWeapon(player);
        await ChooseEquipment(player);
    }
    private async Task ChooseWeapon(Player player)
    {
        WeaponBase startingWeapon = null;
        while (startingWeapon == null)
        {
            string weaponChoice = ParseHelper.AskForString("Choose your starting weapon:\n1. Sword\n2. Axe\n");
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
            "sword" or "1" => await _swordService.GetWeapon(x => x.Name == "Beginner Sword"),
            "axe" or "2" => await _axeService.GetWeapon(x => x.Name == "Beginner Axe"),
            _ => null
        };
    }
}
