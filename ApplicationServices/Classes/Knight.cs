using ApplicationServices.Items.Equipment.Armor;
using ApplicationServices.Items.Equipment.Weapons;
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

    private const string DefaultSwordName = "Iron Shortsword";
    private const string DefaultBootsName = "Bronze Sabatons";
    private const string DefaultChestName = "Bronze Cuirass";
    private const string DefaultGlovesName = "Steel Gauntlets";
    private const string DefaultHelmetName = "Iron Guard Helmet";
    private const string DefaultLegsName = "Bronze Greaves";

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
        var allowedArmor = new List<ArmorMaterial>
        {
            ArmorMaterial.Plate,
            ArmorMaterial.Scale,
            ArmorMaterial.Chain
        };

        var allowedWeapon = new List<WeaponType>
        {
            WeaponType.Sword,
            WeaponType.Axe
        };

        player.SetBaseValues(60, 20, 20, 10, 60, 30, 20, 5, allowedArmor, allowedWeapon);
        await EquipDefaultWeapon(player);
        await EquipDefaultArmor(player);
    }

    private async Task EquipDefaultWeapon(Player player)
    {
        player.MainHand = await _swordService.GetWeapon(x => x.Name == DefaultSwordName);
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
