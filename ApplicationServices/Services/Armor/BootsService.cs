using ApplicationServices.Items.Equipment.Armor.BaseArmors;
using Domain.Entities.Armor.Models;
using Domain.Repos.Armor;
using System.Linq.Expressions;

namespace ApplicationServices.Services.Armor;

public class BootsService(IBootsRepository repo) : IBootsService
{
    public async Task<List<Boots>> GetBoots()
    {
        var boots = await repo.GetAllAsync();
        return boots.Select(b => (Boots)b).ToList();
    }

    public async Task<Boots> GetBoots(Expression<Func<BootsEntity, bool>> predicate)
    {
        var entity = await repo.GetAsync(predicate);
        return entity;
    }

    public async Task<bool> AddBoots(Boots boots)
    {
        return await repo.AddAsync(boots) != null;
    }

    public async Task<bool> UpdateWeapon(Boots boots)
    {
        return await repo.UpdateAsync(boots) != null;
    }

    public async Task<bool> DeleteWeapon(Guid id)
    {
        var entity = await GetBoots(x => x.Id == id);
        await repo.DeleteAsync(entity);
        return true;
    }
}

