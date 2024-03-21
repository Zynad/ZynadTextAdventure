using AutoFixture;
using Domain.Contexts;
using Domain.Entities.Weapons.Models;
using Domain.Enums;
using Domain.Repos.Weapons;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace TextAdventureTests.Repos;
public class WeaponsRepositoryTests
{
    private readonly Fixture _autoFixture = new Fixture();
    public WandRepository GetSut()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase") // Use in-memory database for testing
            .Options;
        var context = new DataContext(options); // Create a new DataContext with the options
        return new WandRepository(context);
    }

    [Fact]
    public async Task GetWeapon_WithNameBeginnerWand_ShouldReturnCorrectItem()
    {
        // Arrange
        var sut = GetSut();
        var wand = _autoFixture.Build<WandEntity>()
            .With(x => x.Name, "Beginner Wand")
            .With(x => x.WeaponType, WeaponTypeEntity.Wand)
            .Create();
        await sut.AddAsync(wand);
        var wandName = "Beginner Wand";
        // Act
        var result = await sut.GetAsync(w => w.Name == wandName);
        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(wandName);
        result.WeaponType.Should().Be(WeaponTypeEntity.Wand);
    }

    [Fact]
    public async Task AddWeapon_AddsWandToList()
    {
        // Arrange
        var sut = GetSut();
        var wand = _autoFixture
            .Build<WandEntity>()
            .With(x => x.WeaponType, WeaponTypeEntity.Wand)
            .Create();
        // Act
        await sut.AddAsync(wand);
        var result = await sut.GetAsync(x => x.Id == wand.Id);
        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task AddWeapon_TempTest()
    {
        // Arrange
        var sut = GetSut();
        var wand = new WandEntity
        {
            Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa7"),
            Name = "Beginner Wand",
            LevelRequirement = 1,
            Rarity = RarityEntity.Common,
            Value = 5,
            Weight = 1,
            Durability = 40,
            Material = WeaponMaterialEntity.Wood,
            WeaponType = WeaponTypeEntity.Wand,
            ArmorValue = 0,
            MeleeAttackValue = 2,
            RangedAttackValue = 8,
            MagicAttackValue = 0,
            IsRanged = true,
            TwoHanded = false,
            Range = 30,
            MagicPower = 30
        };
        // Act
        await sut.AddAsync(wand);
        var result = await sut.GetAsync(x => x.Id == wand.Id);
        // Assert
        result.Should().NotBeNull();
    }
}