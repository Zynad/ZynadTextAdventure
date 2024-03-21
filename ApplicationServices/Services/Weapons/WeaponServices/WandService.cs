using ApplicationServices.Items.Equipment.Weapons.BaseWeapons;
using Domain.Entities.Weapons.Models;
using Domain.Enums;
using Domain.Repos.Weapons;
using System.Linq.Expressions;

namespace ApplicationServices.Services.Weapons.WeaponServices;

public class WandService : IWandService
{
    private readonly IWandRepository _repo;
    public WandService(IWandRepository repo)
    {
        _repo = repo;
    }
    public async Task<List<Wand>> GetWeapons()
    {
        var entities = await _repo.GetAllAsync();
        var sortedEntities = entities.Where(x => x.WeaponType == WeaponTypeEntity.Wand).ToList();
        return sortedEntities.Select(e => (Wand)e).ToList();
    }
    public async Task<Wand> GetWeapon(Expression<Func<WandEntity, bool>> predicate)
    {
        var entity = await _repo.GetAsync(predicate);
        return entity;
    }

    public async Task<bool> AddWeapon(Wand weapon)
    {
        return await _repo.AddAsync(weapon) != null;
    }

    public async Task<bool> UpdateWeapon(Wand weapon)
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


