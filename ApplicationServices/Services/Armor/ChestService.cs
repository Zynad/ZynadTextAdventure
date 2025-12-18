using ApplicationServices.Items.Equipment.Armor.BaseArmors;
using Domain.Entities.Armor.Models;
using Domain.Repos.Armor;
using System.Linq.Expressions;

namespace ApplicationServices.Services.Armor;

public class ChestService(IChestRepository repo) : IChestService
{
    public async Task<List<Chest>> GetChests()
    {
        var chests = await repo.GetAllAsync();
        return chests.Select(c => (Chest)c).ToList();
    }
    public async Task<Chest> GetChest(Expression<Func<ChestEntity, bool>> predicate)
    {
        var entity = await repo.GetAsync(predicate);
        return entity;
    }
    public async Task<bool> AddChest(Chest chest)
    {
        return await repo.AddAsync(chest) != null;
    }
    public async Task<bool> UpdateChest(Chest chest)
    {
        return await repo.UpdateAsync(chest) != null;
    }
    public async Task<bool> DeleteChest(Guid id)
    {
        var entity = await GetChest(x => x.Id == id);
        await repo.DeleteAsync(entity);
        return true;
    }
}

