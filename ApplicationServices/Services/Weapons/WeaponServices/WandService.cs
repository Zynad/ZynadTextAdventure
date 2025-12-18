using ApplicationServices.Items.Equipment.Weapons.BaseWeapons;
using Domain.Entities.Weapons.Models;
using Domain.Enums;
using Domain.Repos.Weapons;
using System.Linq.Expressions;

namespace ApplicationServices.Services.Weapons.WeaponServices;

public class WandService(IWandRepository repo) : IWandService
{
    public async Task<List<Wand>> GetWeapons()
    {
        var entities = await repo.GetAllAsync();
        var sortedEntities = entities.Where(x => x.WeaponType == WeaponTypeEntity.Wand).ToList();
        return sortedEntities.Select(e => (Wand)e).ToList();
    }
    public async Task<Wand> GetWeapon(Expression<Func<WandEntity, bool>> predicate)
    {
        var entity = await repo.GetAsync(predicate);
        return entity;
    }

    public async Task<bool> AddWeapon(Wand weapon)
    {
        return await repo.AddAsync(weapon) != null;
    }

    public async Task<bool> UpdateWeapon(Wand weapon)
    {
        return await repo.UpdateAsync(weapon) != null;
    }

    public async Task<bool> DeleteWeapon(Guid id)
    {
        var entity = await GetWeapon(x => x.Id == id);
        await repo.DeleteAsync(entity);
        return true;
    }
}


