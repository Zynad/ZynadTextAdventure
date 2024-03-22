using System.Linq.Expressions;
using ApplicationServices.Items.Equipment.Weapons.BaseWeapons;
using Domain.Entities.Weapons.Models;

namespace ApplicationServices.Services.Weapons.WeaponServices;

public interface IStaffService
{
    Task<List<Staff>> GetWeapons();
    Task<Staff> GetWeapon(Expression<Func<StaffEntity, bool>> predicate);
    Task<bool> AddWeapon(Staff weapon);
    Task<bool> UpdateWeapon(Staff weapon);
    Task<bool> DeleteWeapon(Guid id);
}

