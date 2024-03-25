using ApplicationServices.Items.Equipment.Weapons.Factories;
using Domain.Entities.Weapons.Models;
using Domain.Repos.Weapons;
namespace ApplicationServices.Game.Helpers;
public class DbHandler : IDbHandler
{
    private readonly IWandRepository _wandRepository;
    private readonly IStaffRepository _staffRepository;
    private readonly IWandFactory _wandFactory;
    public DbHandler(IWandRepository wandRepository, IStaffRepository staffRepository, IWandFactory wandFactory)
    {
        _wandRepository = wandRepository;
        _staffRepository = staffRepository;
        _wandFactory = wandFactory;
    }
    public async Task AddWand(WandEntity entity = null)
    {
        if (entity != null)
        {
            await _wandRepository.AddAsync(entity);
            return;
        }
        var wand = await _wandFactory.CreateNewWand();
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
        await _staffRepository.AddAsync(entity);
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