﻿using TextAdventure.Items;
using TextAdventure.Items.Equipment.Armor;
using TextAdventure.Items.Equipment.Weapons;

namespace TextAdventure.Characters;

public abstract class Creature
{
    public Creature()
    {
        CheckLevel();
    }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Name => $"{FirstName} {LastName}";
    public int Age { get; set; }
    public int HitPoints { get; set; }
    public int ManaPoints { get; set; }
    public int Stamina { get; set; }
    public Gender Gender { get; set; }
    public int ArmorValue { get; set; }
    public int MeleePhysicalAttackDamage { get; set; }
    public int RangedPhysicalAttackDamage { get; set; }
    public int MagicAttackDamage { get; set; }
    public int DefenseValue { get; set; }
    public List<ArmorMaterial> CamCarryArmorType { get; set; }
    public List<WeaponType> CanCarryWeaponType { get; set; }
    public List<ItemsBase> Inventory { get; set; }
    public int MaxCarryWeigth { get; set; }
    private int xpValue;
    public int XpValue
    {
        get => xpValue;
        set
        {
            xpValue = value;
            CheckLevel();
        }
    }
    public int Level { get; private set; }
    private void CheckLevel()
    {
        int oldLevel = Level;
        Level = XpValue switch
        {
            < 100 => 1,
            >= 100 and < 300 => 2,
            >= 300 and < 600 => 3,
            > 600 => 4,
            _ => Level
        };
        // If the level has increased, call LevelUp
        for (int i = oldLevel; i < Level; i++)
        {
            LevelUp();
        }
    }
    public void AddXp(int xp)
    {
        XpValue += xp;
    }

    protected virtual void LevelUp()
    {
    }
}