using TextAdventure.Items.Equipment.Weapons.BaseWeapons;
using TextAdventure.Repos.Weapons;
using TextAdventure.Repos.Weapons.Models;

namespace TextAdventure.Services.Weapons;
public class WeaponsService : IWeaponsService
{
    private readonly IWeaponsRepository _repo;

    public WeaponsService(IWeaponsRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Wand>> GetWands()
    {
        var enteties = await _repo.GetWands();
        return enteties.Select(entity => (Wand)entity).ToList();
    }

    public async Task<Wand> GetWand(Func<WandEntity, bool> predicate)
    {
        var wand = await _repo.GetWand(predicate);
        return wand;
    }
}
