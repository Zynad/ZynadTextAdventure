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

    private const string DefaultStaffName = "Oak Quarterstaff";
    private const string DefaultBootsName = "Mystic Sandals";
    private const string DefaultChestName = "Mystic Robes";
    private const string DefaultGlovesName = "Battlemage Wraps";
    private const string DefaultHelmetName = "Mystic Circlet";
    private const string DefaultLegsName = "Mystic Legwraps";

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
        var allowedArmor = new List<ArmorMaterial> { ArmorMaterial.Cloth };
        var allowedWeapon = new List<WeaponType> { WeaponType.Staff, WeaponType.Wand };

        player.SetBaseValues(30, 10, 5, 50, 30, 15, 10, 30, allowedArmor, allowedWeapon);
        await EquipDefaultWeapon(player);
        await EquipDefaultArmor(player);
    }

    private async Task EquipDefaultWeapon(Player player)
    {
        player.MainHand = await _staffService.GetWeapon(x => x.Name == DefaultStaffName);
    }

    private async Task EquipDefaultArmor(Player player)
    {
        player.Boots = await _bootsService.GetBoots(x => x.Name == DefaultBootsName);
        player.Chest = await _chestService.GetChest(x => x.Name == DefaultChestName);
        player.Gloves = await _glovesService.GetGlove(x => x.Name == DefaultGlovesName);
        player.Helmet = await _helmetService.GetHelmet(x => x.Name == DefaultHelmetName);
        player.Legs = await _legsService.GetLeg(x => x.Name == DefaultLegsName);
    }
}
