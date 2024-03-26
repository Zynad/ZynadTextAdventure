using ApplicationServices.Game.Helpers;
using ApplicationServices.Items.Equipment.Armor;
using ApplicationServices.Items.Equipment.Weapons;
using ApplicationServices.PlayerSettings;
using ApplicationServices.Services.Armor;
using ApplicationServices.Services.Weapons.WeaponServices;

namespace ApplicationServices.Classes;
public class Mage : Vocation
{
    private readonly IWandService _wandService;
    private readonly IStaffService _staffService;
    private readonly IBootsService _bootsService;
    private readonly IChestService _chestService;
    private readonly IGlovesService _glovesService;
    private readonly IHelmetService _helmetService;
    private readonly ILegsService _legsService;
    public Mage(IWandService wandService, IStaffService staffService, IBootsService bootsService, IChestService chestService, IGlovesService glovesService, IHelmetService helmetService, ILegsService legsService)
    {
        _wandService = wandService ?? throw new ArgumentNullException(nameof(wandService));
        _staffService = staffService ?? throw new ArgumentNullException(nameof(staffService));
        _bootsService = bootsService ?? throw new ArgumentNullException(nameof(bootsService));
        _chestService = chestService ?? throw new ArgumentNullException(nameof(chestService));
        _glovesService = glovesService ?? throw new ArgumentNullException(nameof(glovesService));
        _helmetService = helmetService ?? throw new ArgumentNullException(nameof(helmetService));
        _legsService = legsService ?? throw new ArgumentNullException(nameof(legsService));
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
            string weaponChoice = ParseHelper.AskForString("Game Master : Choose your starting weapon:\n1. Staff\n2. Wand\n");
            startingWeapon = await CreateWeapon(weaponChoice);
            if (startingWeapon == null)
            {
                Console.WriteLine("Game Master : Invalid choice");
            }
        }
        player.MainHand = startingWeapon;
        Console.WriteLine($"Game Master : You have chosen a {player.MainHand.Name} as your starting weapon.");
    }

    private async Task ChooseEquipment(Player player)
    {
        player.Boots = await _bootsService.GetBoots(x => x.Name == "Beginner Sandals");
        player.Chest = await _chestService.GetChest(x => x.Name == "Beginner Robe");
        player.Gloves = await _glovesService.GetGlove(x => x.Name == "Beginner Cloth Gloves");
        player.Helmet = await _helmetService.GetHelmet(x => x.Name == "Beginner Cloth Helmet");
        player.Legs = await _legsService.GetLeg(x => x.Name == "Beginner Cloth Legs");
        Console.WriteLine("Game Master : Here is also a set of cloth armor that you should wear, you can inspect them further later on");
        Console.WriteLine($"{player.FirstName} is getting dressed");
        // Wait for 3 seconds
        await Task.Delay(TimeSpan.FromSeconds(3));
        Console.WriteLine("Game Master : They look so good on you!");
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
