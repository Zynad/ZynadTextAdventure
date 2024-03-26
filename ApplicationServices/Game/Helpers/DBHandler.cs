using ApplicationServices.Items.Equipment.Armor.Factories;
using ApplicationServices.Items.Equipment.Weapons.Factories;
using Domain.Entities.Armor.Models;
using Domain.Entities.Weapons.Models;
using Domain.Repos.Armor;
using Domain.Repos.Weapons;
namespace ApplicationServices.Game.Helpers;

public class DbHandler : IDbHandler
{
    private readonly IWandRepository _wandRepository;
    private readonly IStaffRepository _staffRepository;
    private readonly ISwordRepository _swordRepository;
    private readonly IAxeRepository _axeRepository;
    private readonly IBootsRepository _bootsRepository;
    private readonly IChestRepository _chestRepository;
    private readonly IGlovesRepository _glovesRepository;
    private readonly IHelmetRepository _helmetRepository;
    private readonly ILegsRepository _legsRepository;
    private readonly IWandFactory _wandFactory;
    private readonly IStaffFactory _staffFactory;
    private readonly ISwordFactory _swordFactory;
    private readonly IAxeFactory _axeFactory;
    private readonly IBootsFactory _bootsFactory;
    private readonly IChestFactory _chestFactory;
    private readonly IGlovesFactory _glovesFactory;
    private readonly IHelmetFactory _helmetFactory;
    private readonly ILegsFactory _legsFactory;

    public DbHandler(IWandRepository wandRepository, IStaffRepository staffRepository, ISwordRepository swordRepository, IAxeRepository axeRepository, IBootsRepository bootsRepository, IChestRepository chestRepository, IGlovesRepository glovesRepository, IHelmetRepository helmetRepository, ILegsRepository legsRepository, IWandFactory wandFactory, IStaffFactory staffFactory, ISwordFactory swordFactory, IAxeFactory axeFactory, IBootsFactory bootsFactory, IChestFactory chestFactory, IGlovesFactory glovesFactory, IHelmetFactory helmetFactory, ILegsFactory legsFactory)
    {
        _wandRepository = wandRepository;
        _staffRepository = staffRepository;
        _swordRepository = swordRepository;
        _axeRepository = axeRepository;
        _bootsRepository = bootsRepository;
        _chestRepository = chestRepository;
        _glovesRepository = glovesRepository;
        _helmetRepository = helmetRepository;
        _legsRepository = legsRepository;
        _wandFactory = wandFactory;
        _staffFactory = staffFactory;
        _swordFactory = swordFactory;
        _axeFactory = axeFactory;
        _bootsFactory = bootsFactory;
        _chestFactory = chestFactory;
        _glovesFactory = glovesFactory;
        _helmetFactory = helmetFactory;
        _legsFactory = legsFactory;
    }

    #region Wand

    public async Task AddWand(WandEntity entity = null)
    {
        if (entity != null)
        {
            await _wandRepository.AddAsync(entity);
            return;
        }

        var wand = _wandFactory.CreateNewWand();
        await _wandRepository.AddAsync(wand);
    }

    public async Task UpdateWand(WandEntity entity)
    {
        await _wandRepository.UpdateAsync(entity);
    }

    public async Task DeleteWand(WandEntity entity)
    {
        await _wandRepository.DeleteAsync(entity);
    }

    public async Task GetWand()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Staff

    public Task GetStaff()
    {
        throw new NotImplementedException();
    }

    public async Task AddStaff(StaffEntity entity = null)
    {
        if (entity != null)
        {
            await _staffRepository.AddAsync(entity);
            return;
        }

        var staff = _staffFactory.CreateNewStaff();
        await _staffRepository.AddAsync(staff);
    }

    public async Task UpdateStaff(StaffEntity entity)
    {
        await _staffRepository.UpdateAsync(entity);
    }

    public async Task DeleteStaff(StaffEntity entity)
    {
        await _staffRepository.DeleteAsync(entity);
    }

    #endregion

    #region Sword

    public async Task AddSword(SwordEntity entity = null)
    {
        if (entity != null)
        {
            await _swordRepository.AddAsync(entity);
            return;
        }

        var sword = _swordFactory.CreateNewSword();
        await _swordRepository.AddAsync(sword);
    }

    public async Task UpdateSword(SwordEntity entity)
    {
        await _swordRepository.UpdateAsync(entity);
    }

    public async Task DeleteSword(SwordEntity entity)
    {
        await _swordRepository.DeleteAsync(entity);
    }

    public async Task GetSword()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Axe

    public async Task AddAxe(AxeEntity entity = null)
    {
        if (entity != null)
        {
            await _axeRepository.AddAsync(entity);
            return;
        }
        var axe = _axeFactory.CreateNewAxe();
        await _axeRepository.AddAsync(axe);
    }
    public async Task UpdateAxe(AxeEntity entity)
    {
        await _axeRepository.UpdateAsync(entity);
    }
    public async Task DeleteAxe(AxeEntity entity)
    {
        await _axeRepository.DeleteAsync(entity);
    }
    public async Task GetAxe()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Boots
    public async Task AddBoots(BootsEntity entity = null)
    {
        if (entity != null)
        {
            await _bootsRepository.AddAsync(entity);
            return;
        }
        var boots = _bootsFactory.CreateNewBoots();
        await _bootsRepository.AddAsync(boots);
    }
    public async Task UpdateBoots(BootsEntity entity)
    {
        await _bootsRepository.UpdateAsync(entity);
    }
    public async Task DeleteBoots(BootsEntity entity)
    {
        await _bootsRepository.DeleteAsync(entity);
    }
    public async Task GetBoots()
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Chest
    public async Task AddChest(ChestEntity entity = null)
    {
        if (entity != null)
        {
            await _chestRepository.AddAsync(entity);
            return;
        }
        var chest = _chestFactory.CreateNewChest();
        await _chestRepository.AddAsync(chest);
    }
    public async Task UpdateChest(ChestEntity entity)
    {
        await _chestRepository.UpdateAsync(entity);
    }
    public async Task DeleteChest(ChestEntity entity)
    {
        await _chestRepository.DeleteAsync(entity);
    }
    public async Task GetChest()
    {
        throw new NotImplementedException();
    }
    #endregion
    
    #region Gloves
    public async Task AddGloves(GlovesEntity entity = null)
    {
        if (entity != null)
        {
            await _glovesRepository.AddAsync(entity);
            return;
        }
        var gloves = _glovesFactory.CreateNewGloves();
        await _glovesRepository.AddAsync(gloves);
    }
    public async Task UpdateGloves(GlovesEntity entity)
    {
        await _glovesRepository.UpdateAsync(entity);
    }
    public async Task DeleteGloves(GlovesEntity entity)
    {
        await _glovesRepository.DeleteAsync(entity);
    }
    public async Task GetGloves()
    {
        throw new NotImplementedException();
    }
    #endregion
    
    #region Helmet
    public async Task AddHelmet(HelmetEntity entity = null)
    {
        if (entity != null)
        {
            await _helmetRepository.AddAsync(entity);
            return;
        }
        var helmet = _helmetFactory.CreateNewHelmet();
        await _helmetRepository.AddAsync(helmet);
    }
    public async Task UpdateHelmet(HelmetEntity entity)
    {
        await _helmetRepository.UpdateAsync(entity);
    }
    public async Task DeleteHelmet(HelmetEntity entity)
    {
        await _helmetRepository.DeleteAsync(entity);
    }
    public async Task GetHelmet()
    {
        throw new NotImplementedException();
    }
    #endregion
    
    #region Legs
    public async Task AddLegs(LegsEntity entity = null)
    {
        if (entity != null)
        {
            await _legsRepository.AddAsync(entity);
            return;
        }
        var legs = _legsFactory.CreateNewLegs();
        await _legsRepository.AddAsync(legs);
    }
    public async Task UpdateLegs(LegsEntity entity)
    {
        await _legsRepository.UpdateAsync(entity);
    }
    public async Task DeleteLegs(LegsEntity entity)
    {
        await _legsRepository.DeleteAsync(entity);
    }
    public async Task GetLegs()
    {
        throw new NotImplementedException();
    }
    #endregion

}