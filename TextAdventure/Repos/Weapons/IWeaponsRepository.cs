using TextAdventure.Items.Equipment.Weapons;

namespace TextAdventure.Repos.Weapons;
public interface IWeaponRepository<T> where T : WeaponBase
{
    Task<List<T>> GetWeapons();
    Task<T> GetWeapon(Func<T, bool> predicate);
    Task AddWeapon(T weapon);
}

