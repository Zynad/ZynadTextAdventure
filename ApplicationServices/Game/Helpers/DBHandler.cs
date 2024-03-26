using ApplicationServices.Items.Equipment.Weapons.Factories;
using Domain.Entities.Weapons.Models;
using Domain.Repos.Weapons;
namespace ApplicationServices.Game.Helpers;

public class DbHandler : IDbHandler
{
    private readonly IWandRepository _wandRepository;
    private readonly IStaffRepository _staffRepository;
    private readonly ISwordRepository _swordRepository;
    private readonly IAxeRepository _axeRepository;
    private readonly IWandFactory _wandFactory;
    private readonly IStaffFactory _staffFactory;
    private readonly ISwordFactory _swordFactory;
    private readonly IAxeFactory _axeFactory;

    public DbHandler(IWandRepository wandRepository, IStaffRepository staffRepository, IWandFactory wandFactory,
        IStaffFactory staffFactory, ISwordRepository swordRepository, IAxeRepository axeRepository, ISwordFactory swordFactory, IAxeFactory axeFactory)
    {
        _wandRepository = wandRepository;
        _staffRepository = staffRepository;
        _wandFactory = wandFactory;
        _staffFactory = staffFactory;
        _swordRepository = swordRepository;
        _axeRepository = axeRepository;
        _swordFactory = swordFactory;
        _axeFactory = axeFactory;
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
}