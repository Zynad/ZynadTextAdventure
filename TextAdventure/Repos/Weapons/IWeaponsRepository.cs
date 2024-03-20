using TextAdventure.Repos.Weapons.Models;

namespace TextAdventure.Repos.Weapons;
public interface IWeaponRepository<T> where T : WeaponBaseEntity
{
    Task<List<T>> GetWeapons();
    Task<T> GetWeapon(Func<T, bool> predicate);
    Task AddWeapon(T weapon);
}

