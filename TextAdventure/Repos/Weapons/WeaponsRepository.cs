using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TextAdventure.Repos.Weapons.Models;

namespace TextAdventure.Repos.Weapons;
public class WeaponsRepository : IWeaponsRepository
{
    private readonly string _filePath;
    public WeaponsRepository(IConfiguration configuration)
    {
        _filePath = configuration["FilePaths:WeaponsDB"];
    }
    public async Task<List<WandEntity>> GetWands()
    {
        var json = File.ReadAllText(_filePath);
        return JsonConvert.DeserializeObject<List<WandEntity>>(json);
    }

    public async Task<WandEntity> GetWand(Func<WandEntity, bool> predicate)
    {
        var wands = await GetWands();
        return wands.FirstOrDefault(predicate);
    }
    
    public async Task AddWand(WandEntity wand)
    {
        var wands = await GetWands();
        wands.Add(wand);
        var json = JsonConvert.SerializeObject(wands);
        File.WriteAllText(_filePath, json);
    }
}
