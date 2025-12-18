using ApplicationServices.Items.Equipment.Armor.BaseArmors;
using Domain.Entities.Armor.Models;
using Domain.Repos.Armor;
using System.Linq.Expressions;

namespace ApplicationServices.Services.Armor;

public class GlovesService(IGlovesRepository repo) : IGlovesService
{
    public async Task<List<Gloves>> GetGloves()
    {
        var gloves = await repo.GetAllAsync();
        return gloves.Select(g => (Gloves)g).ToList();
    }
    public async Task<Gloves> GetGlove(Expression<Func<GlovesEntity, bool>> predicate)
    {
        var entity = await repo.GetAsync(predicate);
        return entity;
    }
    public async Task<bool> AddGloves(Gloves gloves)
    {
        return await repo.AddAsync(gloves) != null;
    }
    public async Task<bool> UpdateGloves(Gloves gloves)
    {
        return await repo.UpdateAsync(gloves) != null;
    }
    public async Task<bool> DeleteGloves(Guid id)
    {
        var entity = await GetGlove(x => x.Id == id);
        await repo.DeleteAsync(entity);
        return true;
    }
}

