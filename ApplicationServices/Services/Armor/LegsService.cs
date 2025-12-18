using ApplicationServices.Items.Equipment.Armor.BaseArmors;
using Domain.Entities.Armor.Models;
using Domain.Repos.Armor;
using System.Linq.Expressions;

namespace ApplicationServices.Services.Armor;

public class LegsService(ILegsRepository repo) : ILegsService
{
    public async Task<List<Legs>> GetLegs()
    {
        var legs = await repo.GetAllAsync();
        return legs.Select(l => (Legs)l).ToList();
    }
    public async Task<Legs> GetLeg(Expression<Func<LegsEntity, bool>> predicate)
    {
        var entity = await repo.GetAsync(predicate);
        return entity;
    }
    public async Task<bool> AddLegs(Legs legs)
    {
        return await repo.AddAsync(legs) != null;
    }
    public async Task<bool> UpdateLegs(Legs legs)
    {
        return await repo.UpdateAsync(legs) != null;
    }
    public async Task<bool> DeleteLegs(Guid id)
    {
        var entity = await GetLeg(x => x.Id == id);
        await repo.DeleteAsync(entity);
        return true;
    }
}

