using ApplicationServices.Items.Equipment.Armor;
using Domain.Entities.Armor.Models;
using System.Linq.Expressions;

namespace ApplicationServices.Services.Weapons.Armor;

public interface IBootsService
{
    Task<List<Boots>> GetBoots();
    Task<Boots> GetBoots(Expression<Func<BootsEntity, bool>> predicate);
    Task<bool> AddBoots(Boots boots);
    Task<bool> UpdateWeapon(Boots boots);
    Task<bool> DeleteWeapon(Guid id);
}

