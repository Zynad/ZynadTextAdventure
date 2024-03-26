using ApplicationServices.Items.Equipment.Armor;
using Domain.Entities.Armor.Models;
using Domain.Repos.Armor;
using System.Linq.Expressions;

namespace ApplicationServices.Services.Weapons.Armor;

public class GlovesService : IGlovesService
{
    private readonly IGlovesRepository _repo;
    public GlovesService(IGlovesRepository repo)
    {
        _repo = repo;
    }
    public async Task<List<Gloves>> GetGloves()
    {
        var gloves = await _repo.GetAllAsync();
        return gloves.Select(g => (Gloves)g).ToList();
    }
    public async Task<Gloves> GetGlove(Expression<Func<GlovesEntity, bool>> predicate)
    {
        var entity = await _repo.GetAsync(predicate);
        return entity;
    }
    public async Task<bool> AddGloves(Gloves gloves)
    {
        return await _repo.AddAsync(gloves) != null;
    }
    public async Task<bool> UpdateGloves(Gloves gloves)
    {
        return await _repo.UpdateAsync(gloves) != null;
    }
    public async Task<bool> DeleteGloves(Guid id)
    {
        var entity = await GetGlove(x => x.Id == id);
        await _repo.DeleteAsync(entity);
        return true;
    }
}

