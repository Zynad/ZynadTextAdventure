using ApplicationServices.Items.Equipment.Armor;
using Domain.Entities.Armor.Models;
using System.Linq.Expressions;

namespace ApplicationServices.Services.Weapons.Armor;

public interface IHelmetService
{
    Task<List<Helmet>> GetHelmets();
    Task<Helmet> GetHelmet(Expression<Func<HelmetEntity, bool>> predicate);
    Task<bool> AddHelmet(Helmet helmet);
    Task<bool> UpdateHelmet(Helmet helmet);
    Task<bool> DeleteHelmet(Guid id);
}

