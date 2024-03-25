using ApplicationServices.Items.Equipment.Weapons.Factories;
using Domain.Entities.Weapons.Models;
using Domain.Repos.Weapons;
namespace ApplicationServices.Game.Helpers;
public class DbHandler : IDbHandler
{
    private readonly IWandRepository _wandRepository;
    private readonly IStaffRepository _staffRepository;
    private readonly IWandFactory _wandFactory;
    private readonly IStaffFactory _staffFactory;
    public DbHandler(IWandRepository wandRepository, IStaffRepository staffRepository, IWandFactory wandFactory, IStaffFactory staffFactory)
    {
        _wandRepository = wandRepository;
        _staffRepository = staffRepository;
        _wandFactory = wandFactory;
        _staffFactory = staffFactory;
    }
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
}