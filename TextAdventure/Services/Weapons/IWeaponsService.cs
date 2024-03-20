using TextAdventure.Items.Equipment.Weapons.BaseWeapons;
using TextAdventure.Repos.Weapons.Models;

namespace TextAdventure.Services.Weapons;
public interface IWeaponsService
{
    Task<Wand> GetWand(Func<WandEntity, bool> predicate);
    Task<List<Wand>> GetWands();
}
