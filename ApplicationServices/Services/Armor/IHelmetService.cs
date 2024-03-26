using ApplicationServices.Items.Equipment.Armor.BaseArmors;
using Domain.Entities.Armor.Models;
using System.Linq.Expressions;

namespace ApplicationServices.Services.Armor;

public interface IHelmetService
{
    Task<List<Helmet>> GetHelmets();
    Task<Helmet> GetHelmet(Expression<Func<HelmetEntity, bool>> predicate);
    Task<bool> AddHelmet(Helmet helmet);
    Task<bool> UpdateHelmet(Helmet helmet);
    Task<bool> DeleteHelmet(Guid id);
}

