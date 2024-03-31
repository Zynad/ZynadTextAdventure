using ApplicationServices.Game.Helpers;
using ApplicationServices.Items.Equipment.Armor;
using ApplicationServices.Items.Equipment.Weapons;
using ApplicationServices.Items.Equipment.Weapons.BaseWeapons;
using ApplicationServices.PlayerSettings;
using ApplicationServices.Services.Armor;
using ApplicationServices.Services.Weapons.WeaponServices;

namespace ApplicationServices.Classes;
public class Knight : Vocation
{
    private readonly ISwordService _swordService;
    private readonly IAxeService _axeService;
    private readonly IBootsService _bootsService;
    private readonly IChestService _chestService;
    private readonly IGlovesService _glovesService;
    private readonly IHelmetService _helmetService;
    private readonly ILegsService _legsService;
    public Knight(IAxeService axeService, ISwordService swordService, IBootsService bootsService, IChestService chestService, IGlovesService glovesService, IHelmetService helmetService, ILegsService legsService)
    {
        _axeService = axeService ?? throw new ArgumentNullException(nameof(axeService));
        _swordService = swordService ?? throw new ArgumentNullException(nameof(swordService));
        _bootsService = bootsService ?? throw new ArgumentNullException(nameof(bootsService));
        _chestService = chestService ?? throw new ArgumentNullException(nameof(chestService));
        _glovesService = glovesService ?? throw new ArgumentNullException(nameof(glovesService));
        _helmetService = helmetService ?? throw new ArgumentNullException(nameof(helmetService));
        _legsService = legsService ?? throw new ArgumentNullException(nameof(legsService));
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
        // TODO : Change this to the correct entities for knight
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
            "sword" or "1" => await _swordService.GetWeapon(x => x.Name == "Beginner Sword"),
            "axe" or "2" => await _axeService.GetWeapon(x => x.Name == "Beginner Axe"),
            _ => null
        };
    }
}
