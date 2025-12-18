using ApplicationServices.Items.Equipment.Weapons.BaseWeapons;
using Domain.Entities.Weapons.Models;
using Domain.Enums;
using Shouldly;
using Xunit;

namespace TextAdventureTests.Items;

public class WeaponMappingTests
{
    [Fact]
    public void SwordEntity_ToModel_PreservesOffensiveStats()
    {
        var entity = new SwordEntity
        {
            Name = "Frostbite",
            Material = WeaponMaterialEntity.Steel,
            WeaponType = WeaponTypeEntity.Sword,
            MeleeAttackValue = 15,
            RangedAttackValue = 2,
            MagicAttackValue = 6,
            MagicPower = 4,
            IsRanged = false,
            TwoHanded = true,
            Range = 0,
            Rarity = RarityEntity.Epic
        };

        Sword model = entity;

        model.MeleeAttackValue.ShouldBe(entity.MeleeAttackValue);
        model.RangedAttackValue.ShouldBe(entity.RangedAttackValue);
        model.MagicAttackValue.ShouldBe(entity.MagicAttackValue);
        model.MagicPower.ShouldBe(entity.MagicPower);
        model.TwoHanded.ShouldBeTrue();
        model.IsRanged.ShouldBeFalse();
        model.WeaponType.ShouldBe(ApplicationServices.Items.Equipment.Weapons.WeaponType.Sword);
    }

    [Fact]
    public void SwordModel_ToEntity_PreservesOffensiveStats()
    {
        var model = new Sword
        {
            Name = "Stormsong",
            Material = ApplicationServices.Items.Equipment.Weapons.WeaponMaterial.Adamantium,
            MeleeAttackValue = 22,
            RangedAttackValue = 0,
            MagicAttackValue = 11,
            MagicPower = 8,
            IsRanged = false,
            TwoHanded = false,
            Range = 1,
            Rarity = ApplicationServices.Items.Rarity.Legendary,
        };

        SwordEntity entity = model;

        entity.MeleeAttackValue.ShouldBe(model.MeleeAttackValue);
        entity.RangedAttackValue.ShouldBe(model.RangedAttackValue);
        entity.MagicAttackValue.ShouldBe(model.MagicAttackValue);
        entity.MagicPower.ShouldBe(model.MagicPower);
        entity.WeaponType.ShouldBe(WeaponTypeEntity.Sword);
    }
}
