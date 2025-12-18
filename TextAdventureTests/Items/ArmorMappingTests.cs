using ApplicationServices.Items.Equipment.Armor.BaseArmors;
using Domain.Entities.Armor.Models;
using Domain.Enums;
using Shouldly;
using Xunit;

namespace TextAdventureTests.Items;

public class ArmorMappingTests
{
    [Fact]
    public void HelmetEntity_ToModel_PreservesDefensiveStats()
    {
        var entity = new HelmetEntity
        {
            Name = "Iron Helm",
            Material = ArmorMaterialEntity.Plate,
            PhysicalDefense = 8,
            MagicResistance = 3,
            Durability = 30,
            Weight = 5,
            LevelRequirement = 2,
            Rarity = RarityEntity.Rare,
            Value = 120
        };

        Helmet model = entity;

        model.PhysicalDefense.ShouldBe(entity.PhysicalDefense);
        model.MagicResistance.ShouldBe(entity.MagicResistance);
        model.Material.ShouldBe((ApplicationServices.Items.Equipment.Armor.ArmorMaterial)entity.Material);
        model.Rarity.ShouldBe((ApplicationServices.Items.Rarity)entity.Rarity);
    }

    [Fact]
    public void HelmetModel_ToEntity_PreservesDefensiveStats()
    {
        var model = new Helmet
        {
            Name = "Arcane Visor",
            Material = ApplicationServices.Items.Equipment.Armor.ArmorMaterial.Titanium,
            PhysicalDefense = 5,
            MagicResistance = 9,
            Durability = 45,
            Weight = 4,
            LevelRequirement = 5,
            Rarity = ApplicationServices.Items.Rarity.Legendary,
            Value = 300
        };

        HelmetEntity entity = model;

        entity.PhysicalDefense.ShouldBe(model.PhysicalDefense);
        entity.MagicResistance.ShouldBe(model.MagicResistance);
        entity.Material.ShouldBe((ArmorMaterialEntity)model.Material);
        entity.Rarity.ShouldBe((RarityEntity)model.Rarity);
    }
}
