using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TextAdventure.Items.Equipment.Weapons;
using TextAdventure.Repos.Weapons.Models;

namespace TextAdventure.Repos.Weapons.WeaponRepos;

public class WandRepository : IWandRepository
{
    private readonly string _filePath;
    public WandRepository(IConfiguration configuration)
    {
        _filePath = configuration["FilePaths:WeaponsDB"];
    }
    public async Task<List<WandEntity>> GetWeapons()
    {
        var json = File.ReadAllText(_filePath);
        var items = JsonConvert.DeserializeObject<List<WandEntity>>(json);
        return items.Where(x => x.WeaponType == WeaponType.Wand).ToList();
    }
    public async Task<WandEntity> GetWeapon(Func<WandEntity, bool> predicate)
    {
        var weapons = await GetWeapons();
        return weapons.FirstOrDefault(predicate);
    }
    public async Task AddWeapon(WandEntity weapon)
    {
        var weapons = await GetWeapons();
        weapons.Add(weapon);
        var json = JsonConvert.SerializeObject(weapons);
        File.WriteAllText(_filePath, json);
    }
}

