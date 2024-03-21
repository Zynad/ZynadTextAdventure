using Domain.Entities.Weapons.Models;
using Domain.Repos.Weapons;
namespace ApplicationServices.Game.Helpers;
public class DBHandler
{
    private readonly IWandRepository _wandRepository;
    private readonly IStaffRepository _staffRepository;
    public DBHandler(IWandRepository wandRepository, IStaffRepository staffRepository)
    {
        _wandRepository = wandRepository;
        _staffRepository = staffRepository;
    }
    public async Task AddWand(WandEntity entity)
    {
        await _wandRepository.AddAsync(entity);
    }
    public async Task UpdateWand(WandEntity entity)
    {
        await _wandRepository.UpdateAsync(entity);
    }
    public async Task DeleteWand(WandEntity entity)
    {
        await _wandRepository.DeleteAsync(entity);
    }
    public async Task AddStaff(StaffEntity entity)
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