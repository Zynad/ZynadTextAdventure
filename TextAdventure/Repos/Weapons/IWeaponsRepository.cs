using TextAdventure.Items.Equipment.Weapons;
using TextAdventure.Repos.Weapons.Models;

namespace TextAdventure.Repos.Weapons;
public interface IWeaponsRepository
{
    Task<List<WeaponBaseEntity>> GetAllWeapons();
    Task<T> GetWeapon<T>(Func<T, bool> predicate, WeaponType type);
    Task<bool> AddWeapon(WeaponBaseEntity weapon);
    Task<List<T>> GetWeapons<T>(WeaponType type);
    Task<bool> UpdateWeapon(WeaponBaseEntity weapon);
    Task<bool> DeleteWeapon(Guid id);
}

