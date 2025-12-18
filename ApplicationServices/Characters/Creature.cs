using ApplicationServices.Items;
using ApplicationServices.Items.Equipment.Armor;
using ApplicationServices.Items.Equipment.Weapons;

namespace ApplicationServices.Characters;

public abstract class Creature
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Name => $"{FirstName} {LastName}";
    public int Age { get; set; }
    private int hp;
    public int HitPoints
    {
        get => hp;
        set
        {
            hp = value;
            if (value <= 0)
            {
                OnDeath();
            }
        }
    }
    public bool IsDead => HitPoints <= 0;
    public int ManaPoints { get; set; }
    public int Stamina { get; set; }
    public Gender Gender { get; set; }
    public int PhysicalDefense { get; set; }
    public int MagicResistance { get; set; }
    public int MeleePhysicalAttackDamage { get; set; }
    public int RangedPhysicalAttackDamage { get; set; }
    public int MagicAttackDamage { get; set; }
    public int DefenseValue { get; set; }
    public List<ArmorMaterial> CanCarryArmorType { get; set; } = [];
    public List<WeaponType> CanCarryWeaponType { get; set; } = [];
    public List<ItemsBase> Inventory { get; set; } = [];
    public int MaxCarryWeigth { get; set; }
    public int WorthXp { get; set; }
    public int Experience { get; set; }
    public int Level { get; set; }

    protected virtual void OnDeath()
    {
    }
}