using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TextAdventure.Items.Equipment.Weapons;
using TextAdventure.Repos.Weapons.Models;

namespace TextAdventure.Repos.Weapons;

public class WeaponsRepository : IWeaponsRepository
{
    private readonly string _filePath;
    public WeaponsRepository(IConfiguration configuration)
    {
        _filePath = configuration["FilePaths:WeaponsDB"];
    }
    public async Task<List<WeaponBaseEntity>> GetAllWeapons()
    {
        var json = await File.ReadAllTextAsync(_filePath);
        return JsonConvert.DeserializeObject<List<WeaponBaseEntity>>(json);
    }

    public async Task<T> GetWeapon<T>(Func<T, bool> predicate, WeaponType type)
    {
        var weapons = await GetWeapons<T>(type);
        return weapons.FirstOrDefault(predicate);
    }

    public async Task<bool> AddWeapon(WeaponBaseEntity weapon)
    {
        var weapons = await GetAllWeapons();
        weapons.Add(weapon);
        var json = JsonConvert.SerializeObject(weapons);
        File.WriteAllText(_filePath, json);
        return true;
    }

    public async Task<List<T>> GetWeapons<T>(WeaponType type)
    {
        var weapons = await GetAllWeapons<T>(type);
        return weapons.OfType<T>().ToList();
    }

    public async Task<bool> UpdateWeapon(WeaponBaseEntity weapon)
    {
        var weapons = await GetAllWeapons();
        var index = weapons.FindIndex(w => w.Id == weapon.Id);
        if (index == -1)
        {
            return false; // Weapon not found
        }
        weapons[index] = weapon; // Update the weapon
        var json = JsonConvert.SerializeObject(weapons);
        await File.WriteAllTextAsync(_filePath, json);
        return true;
    }

    public async Task<bool> DeleteWeapon(Guid id)
    {
        var weapons = await GetAllWeapons();
        var index = weapons.FindIndex(w => w.Id == id);
        if (index == -1)
        {
            return false; // Weapon not found
        }
        weapons.RemoveAt(index); // Delete the weapon
        var json = JsonConvert.SerializeObject(weapons);
        await File.WriteAllTextAsync(_filePath, json);
        return true;
    }

    private async Task<List<T>> GetAllWeapons<T>(WeaponType type)
    {
        var json = await File.ReadAllTextAsync(_filePath);
        return JsonConvert.DeserializeObject<List<T>>(json);
    }
}

