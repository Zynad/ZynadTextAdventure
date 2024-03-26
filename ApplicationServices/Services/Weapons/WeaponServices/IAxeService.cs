using System.Linq.Expressions;
using ApplicationServices.Items.Equipment.Weapons.BaseWeapons;
using Domain.Entities.Weapons.Models;

namespace ApplicationServices.Services.Weapons.WeaponServices;

public interface IAxeService
{
    Task<List<Axe>> GetWeapons();
    Task<Axe> GetWeapon(Expression<Func<AxeEntity, bool>> predicate);
    Task<bool> AddWeapon(Axe weapon);
    Task<bool> UpdateWeapon(Axe weapon);
    Task<bool> DeleteWeapon(Guid id);
}

