using TextAdventure.Items.Equipment.Weapons.BaseWeapons;
using TextAdventure.Repos.Weapons.WeaponRepos;

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
        return entities.Select(e => (Wand)e).ToList();
    }
    public async Task<Wand> GetWeapon(Func<Wand, bool> predicate)
    {
        var entity = await _repo.GetWeapon(e => predicate((Wand)e));
        return entity;
    }
}


