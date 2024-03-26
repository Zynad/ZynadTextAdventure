using ApplicationServices.Items.Equipment.Armor.BaseArmors;
using Domain.Entities.Armor.Models;
using Domain.Repos.Armor;
using System.Linq.Expressions;

namespace ApplicationServices.Services.Armor;

public class HelmetService : IHelmetService
{
    private readonly IHelmetRepository _repo;
    public HelmetService(IHelmetRepository repo)
    {
        _repo = repo;
    }
    public async Task<List<Helmet>> GetHelmets()
    {
        var helmets = await _repo.GetAllAsync();
        return helmets.Select(h => (Helmet)h).ToList();
    }
    public async Task<Helmet> GetHelmet(Expression<Func<HelmetEntity, bool>> predicate)
    {
        var entity = await _repo.GetAsync(predicate);
        return entity;
    }
    public async Task<bool> AddHelmet(Helmet helmet)
    {
        return await _repo.AddAsync(helmet) != null;
    }
    public async Task<bool> UpdateHelmet(Helmet helmet)
    {
        return await _repo.UpdateAsync(helmet) != null;
    }
    public async Task<bool> DeleteHelmet(Guid id)
    {
        var entity = await GetHelmet(x => x.Id == id);
        await _repo.DeleteAsync(entity);
        return true;
    }
}

