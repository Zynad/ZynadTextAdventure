using TextAdventure.Items.Equipment.Weapons;
using TextAdventure.Repos.Weapons.Models;

public interface IWeaponService<TModel, TEntity>
    where TModel : WeaponBase
    where TEntity : WeaponBaseEntity
{
    Task<List<TModel>> GetWeapons();
    Task<TModel> GetWeapon(Func<TEntity, bool> predicate);
    Task<bool> AddWeapon(TModel weapon);
    Task<bool> UpdateWeapon(TModel weapon);
    Task<bool> DeleteWeapon(Guid id);
}