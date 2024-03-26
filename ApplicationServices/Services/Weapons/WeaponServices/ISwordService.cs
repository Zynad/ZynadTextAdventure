using System.Linq.Expressions;
using ApplicationServices.Items.Equipment.Weapons.BaseWeapons;
using Domain.Entities.Weapons.Models;

namespace ApplicationServices.Services.Weapons.WeaponServices;

public interface ISwordService
{
    Task<List<Sword>> GetWeapons();
    Task<Sword> GetWeapon(Expression<Func<SwordEntity, bool>> predicate);
    Task<bool> AddWeapon(Sword weapon);
    Task<bool> UpdateWeapon(Sword weapon);
    Task<bool> DeleteWeapon(Guid id);
}

