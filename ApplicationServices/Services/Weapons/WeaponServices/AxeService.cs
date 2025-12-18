using System.Linq.Expressions;
using ApplicationServices.Items.Equipment.Weapons.BaseWeapons;
using Domain.Entities.Weapons.Models;
using Domain.Enums;
using Domain.Repos.Weapons;

namespace ApplicationServices.Services.Weapons.WeaponServices;

public class AxeService(IAxeRepository repo) : IAxeService
{
    public async Task<List<Axe>> GetWeapons()
    {
        var entities = await repo.GetAllAsync();
        var sortedEntities = entities.Where(x => x.WeaponType == WeaponTypeEntity.Axe).ToList();
        return sortedEntities.Select(e => (Axe)e).ToList();
    }
    public async Task<Axe> GetWeapon(Expression<Func<AxeEntity, bool>> predicate)
    {
        var entity = await repo.GetAsync(predicate);
        return entity;
    }
    public async Task<bool> AddWeapon(Axe weapon)
    {
        return await repo.AddAsync(weapon) != null;
    }
    public async Task<bool> UpdateWeapon(Axe weapon)
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

