using TextAdventure.Items.Equipment.Weapons;
using TextAdventure.Items.Equipment.Weapons.BaseWeapons;
using TextAdventure.Repos.Weapons;
using TextAdventure.Repos.Weapons.Models;
using TextAdventure.Repos.Weapons.SpecificRepo;

namespace TextAdventure.Services.Weapons.WeaponServices;

public class WandService : IWandService
{
    private readonly IWandRepository _repo;
    public WandService(IWandRepository repo)
    {
        _repo = repo;
    }
    public async Task<List<Wand>> GetWeapons()
    {
        var entities = await _repo.GetWeapons();
        var sortedEntities = entities.Where(x => x.WeaponType == WeaponType.Wand).ToList();
        return sortedEntities.Select(e => (Wand)e).ToList();
    }
    public async Task<Wand> GetWeapon(Func<WandEntity, bool> predicate)
    {
        var entity = await _repo.GetWeapon(predicate);
        return entity;
    }

    public async Task<bool> AddWeapon(Wand weapon)
    {
        return await _repo.AddWeapon((WandEntity)weapon);
    }

    public async Task<bool> UpdateWeapon(Wand weapon)
    {
        return await _repo.UpdateWeapon((WandEntity)weapon);
    }

    public async Task<bool> DeleteWeapon(Guid id)
    {
        return await _repo.DeleteWeapon(id);
    }
}


