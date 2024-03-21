using TextAdventure.Repos.Weapons.Models;

namespace TextAdventure.Repos.Weapons.SpecificRepo;

public interface IWandRepository
{
    Task<List<WeaponBaseEntity>> GetAllWeapons();
    Task<WandEntity> GetWeapon(Func<WandEntity, bool> predicate);
    Task<bool> AddWeapon(WeaponBaseEntity weapon);
    Task<List<WandEntity>> GetWeapons();
    Task<bool> UpdateWeapon(WeaponBaseEntity weapon);
    Task<bool> DeleteWeapon(Guid id);
}

