﻿using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TextAdventure.Items.Equipment.Weapons;
using TextAdventure.Repos.Weapons.Models;

namespace TextAdventure.Repos.Weapons.SpecificRepo;

public class WandRepository : IWandRepository
{
    private readonly string _filePath;

    public WandRepository(IConfiguration configuration)
    {
        _filePath = configuration["FilePaths:WeaponsDB"];
    }

    public async Task<List<WeaponBaseEntity>> GetAllWeapons()
    {
        var json = await File.ReadAllTextAsync(_filePath);
        return JsonConvert.DeserializeObject<List<WeaponBaseEntity>>(json);
    }

    public async Task<WandEntity> GetWeapon(Func<WandEntity, bool> predicate)
    {
        var weapons = await GetWeapons();
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

    public async Task<List<WandEntity>> GetWeapons()
    {
        var json = await File.ReadAllTextAsync(_filePath);
        var entities = JsonConvert.DeserializeObject<List<WandEntity>>(json);
        return entities.Where(x => x.WeaponType == WeaponType.Wand).ToList();
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
}

