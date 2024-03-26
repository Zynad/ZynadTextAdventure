using ApplicationServices.Items.Equipment.Armor;
using Domain.Entities.Armor.Models;
using System.Linq.Expressions;

namespace ApplicationServices.Services.Weapons.Armor;

public interface IChestService
{
    Task<List<Chest>> GetChests();
    Task<Chest> GetChest(Expression<Func<ChestEntity, bool>> predicate);
    Task<bool> AddChest(Chest chest);
    Task<bool> UpdateChest(Chest chest);
    Task<bool> DeleteChest(Guid id);
}

