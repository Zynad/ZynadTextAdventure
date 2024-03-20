using TextAdventure.Repos.Weapons.Models;

namespace TextAdventure.Repos.Weapons;
public interface IWeaponsRepository
{
    Task<List<WandEntity>> GetWands();
    Task AddWand(WandEntity wand);
    Task<WandEntity> GetWand(Func<WandEntity, bool> predicate);
}
