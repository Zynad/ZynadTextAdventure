using ApplicationServices.Items.Equipment.Weapons.BaseWeapons;
using Domain.Entities.Weapons.Models;
using Domain.Enums;
using Domain.Repos.Weapons;
using System.Linq.Expressions;

namespace ApplicationServices.Services.Weapons.WeaponServices;

public class StaffService : IStaffService
{
    private readonly IStaffRepository _repo;
    public StaffService(IStaffRepository repo)
    {
        _repo = repo;
    }
    public async Task<List<Staff>> GetWeapons()
    {
        var entities = await _repo.GetAllAsync();
        var sortedEntities = entities.Where(x => x.WeaponType == WeaponTypeEntity.Staff).ToList();
        return sortedEntities.Select(e => (Staff)e).ToList();
    }
    public async Task<Staff> GetWeapon(Expression<Func<StaffEntity, bool>> predicate)
    {
        var entity = await _repo.GetAsync(predicate);
        return entity;
    }

    public async Task<bool> AddWeapon(Staff weapon)
    {
        return await _repo.AddAsync(weapon) != null;
    }

    public async Task<bool> UpdateWeapon(Staff weapon)
    {
        return await _repo.UpdateAsync(weapon) != null;
    }

    public async Task<bool> DeleteWeapon(Guid id)
    {
        var entity = await GetWeapon(x => x.Id == id);
        await _repo.DeleteAsync(entity);
        return true;
    }
}


