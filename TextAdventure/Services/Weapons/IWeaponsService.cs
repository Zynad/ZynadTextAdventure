using TextAdventure.Items.Equipment.Weapons;

public interface IWeaponService<T> where T : WeaponBase
{
    Task<List<T>> GetWeapons();
    Task<T> GetWeapon(Func<T, bool> predicate);
    Task<bool> AddWeapon(T weapon);
    Task<bool> UpdateWeapon(T weapon);
    Task<bool> DeleteWeapon(Guid id);
}