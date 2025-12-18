using System.Linq.Expressions;
using ApplicationServices.Items.Equipment.Weapons.BaseWeapons;
using Domain.Entities.Weapons.Models;
using Domain.Enums;
using Domain.Repos.Weapons;

namespace ApplicationServices.Services.Weapons.WeaponServices;

public class SwordService(ISwordRepository repo) : ISwordService
{
    public async Task<List<Sword>> GetWeapons()
    {
        var entities = await repo.GetAllAsync();
        var sortedEntities = entities.Where(x => x.WeaponType == WeaponTypeEntity.Sword).ToList();
        return sortedEntities.Select(e => (Sword)e).ToList();
    }
    public async Task<Sword> GetWeapon(Expression<Func<SwordEntity, bool>> predicate)
    {
        var entity = await repo.GetAsync(predicate);
        return entity;
    }
    public async Task<bool> AddWeapon(Sword weapon)
    {
        return await repo.AddAsync(weapon) != null;
    }
    public async Task<bool> UpdateWeapon(Sword weapon)
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

