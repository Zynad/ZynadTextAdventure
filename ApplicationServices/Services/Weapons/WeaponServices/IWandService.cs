using System.Linq.Expressions;
using ApplicationServices.Items.Equipment.Weapons.BaseWeapons;
using Domain.Entities.Weapons.Models;

namespace ApplicationServices.Services.Weapons.WeaponServices;

public interface IWandService
{
    Task<List<Wand>> GetWeapons();
    Task<Wand> GetWeapon(Expression<Func<WandEntity, bool>> predicate);
    Task<bool> AddWeapon(Wand weapon);
    Task<bool> UpdateWeapon(Wand weapon);
    Task<bool> DeleteWeapon(Guid id);
}

