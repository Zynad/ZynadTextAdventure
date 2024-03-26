using System.Linq.Expressions;
using ApplicationServices.Items.Equipment.Weapons.BaseWeapons;
using Domain.Entities.Weapons.Models;
using Domain.Enums;
using Domain.Repos.Weapons;

namespace ApplicationServices.Services.Weapons.WeaponServices;

public class SwordService : ISwordService
{
    private readonly ISwordRepository _repo;
    public SwordService(ISwordRepository repo)
    {
        _repo = repo;
    }
    public async Task<List<Sword>> GetWeapons()
    {
        var entities = await _repo.GetAllAsync();
        var sortedEntities = entities.Where(x => x.WeaponType == WeaponTypeEntity.Sword).ToList();
        return sortedEntities.Select(e => (Sword)e).ToList();
    }
    public async Task<Sword> GetWeapon(Expression<Func<SwordEntity, bool>> predicate)
    {
        var entity = await _repo.GetAsync(predicate);
        return entity;
    }
    public async Task<bool> AddWeapon(Sword weapon)
    {
        return await _repo.AddAsync(weapon) != null;
    }
    public async Task<bool> UpdateWeapon(Sword weapon)
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

