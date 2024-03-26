using ApplicationServices.Items.Equipment.Armor.BaseArmors;
using Domain.Entities.Armor.Models;
using Domain.Repos.Armor;
using System.Linq.Expressions;

namespace ApplicationServices.Services.Armor;

public class LegsService : ILegsService
{
    private readonly ILegsRepository _repo;
    public LegsService(ILegsRepository repo)
    {
        _repo = repo;
    }
    public async Task<List<Legs>> GetLegs()
    {
        var legs = await _repo.GetAllAsync();
        return legs.Select(l => (Legs)l).ToList();
    }
    public async Task<Legs> GetLeg(Expression<Func<LegsEntity, bool>> predicate)
    {
        var entity = await _repo.GetAsync(predicate);
        return entity;
    }
    public async Task<bool> AddLegs(Legs legs)
    {
        return await _repo.AddAsync(legs) != null;
    }
    public async Task<bool> UpdateLegs(Legs legs)
    {
        return await _repo.UpdateAsync(legs) != null;
    }
    public async Task<bool> DeleteLegs(Guid id)
    {
        var entity = await GetLeg(x => x.Id == id);
        await _repo.DeleteAsync(entity);
        return true;
    }
}

