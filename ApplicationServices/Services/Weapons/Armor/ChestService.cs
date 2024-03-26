using ApplicationServices.Items.Equipment.Armor;
using Domain.Entities.Armor.Models;
using Domain.Repos.Armor;
using System.Linq.Expressions;

namespace ApplicationServices.Services.Weapons.Armor;

public class ChestService : IChestService
{
    private readonly IChestRepository _repo;
    public ChestService(IChestRepository repo)
    {
        _repo = repo;
    }
    public async Task<List<Chest>> GetChests()
    {
        var chests = await _repo.GetAllAsync();
        return chests.Select(c => (Chest)c).ToList();
    }
    public async Task<Chest> GetChest(Expression<Func<ChestEntity, bool>> predicate)
    {
        var entity = await _repo.GetAsync(predicate);
        return entity;
    }
    public async Task<bool> AddChest(Chest chest)
    {
        return await _repo.AddAsync(chest) != null;
    }
    public async Task<bool> UpdateChest(Chest chest)
    {
        return await _repo.UpdateAsync(chest) != null;
    }
    public async Task<bool> DeleteChest(Guid id)
    {
        var entity = await GetChest(x => x.Id == id);
        await _repo.DeleteAsync(entity);
        return true;
    }
}

