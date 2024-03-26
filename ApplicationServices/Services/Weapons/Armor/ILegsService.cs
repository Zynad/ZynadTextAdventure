using ApplicationServices.Items.Equipment.Armor;
using Domain.Entities.Armor.Models;
using System.Linq.Expressions;

namespace ApplicationServices.Services.Weapons.Armor;

public interface ILegsService
{
    Task<List<Legs>> GetLegs();
    Task<Legs> GetLeg(Expression<Func<LegsEntity, bool>> predicate);
    Task<bool> AddLegs(Legs legs);
    Task<bool> UpdateLegs(Legs legs);
    Task<bool> DeleteLegs(Guid id);
}

