using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TextAdventure.Items.Equipment.Weapons;
using TextAdventure.Repos.Weapons;

public class WeaponRepository<T> : IWeaponRepository<T> where T : WeaponBase
{
    private readonly string _filePath;
    public WeaponRepository(IConfiguration configuration)
    {
        _filePath = configuration["FilePaths:WeaponsDB"];
    }
    public async Task<List<T>> GetWeapons()
    {
        var json = File.ReadAllText(_filePath);
        return JsonConvert.DeserializeObject<List<T>>(json);
    }
    public async Task<T> GetWeapon(Func<T, bool> predicate)
    {
        var weapons = await GetWeapons();
        return weapons.FirstOrDefault(predicate);
    }
    public async Task AddWeapon(T weapon)
    {
        var weapons = await GetWeapons();
        weapons.Add(weapon);
        var json = JsonConvert.SerializeObject(weapons);
        File.WriteAllText(_filePath, json);
    }
}