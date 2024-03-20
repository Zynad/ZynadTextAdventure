using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using TextAdventure.Items.Equipment.Weapons;
using TextAdventure.Repos.Weapons;
using TextAdventure.Repos.Weapons.Models;

namespace TextAdventureTests.Repos;
public class WeaponsRepositoryTests
{
    private readonly Fixture _autoFixture = new Fixture();
    public WeaponsRepository GetSut()
    {
        var configuration = Substitute.For<IConfiguration>();
        configuration["FilePaths:WeaponsDB"].Returns("C:\\Users\\Zynad\\OneDrive\\Kul Kod\\Adventure\\TextAdventure\\TextAdventure\\Repos\\Weapons\\DB\\WeaponsDBTests.json");
        return new WeaponsRepository(configuration);
    }

    [Fact]
    public async Task GetWeapon_WithNameBeginnerWand_ShouldReturnCorrectItem()
    {
        // Arrange
        var sut = GetSut();
        var wandName = "Beginner Wand";
        // Act
        var result = await sut.GetWeapon<WandEntity>(w => w.Name == wandName);
        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(wandName);
    }

    [Fact]
    public async Task AddWeapon_AddsWandToList()
    {
        // Arrange
        var sut = GetSut();
        var wand = _autoFixture
            .Build<WandEntity>()
            .With(x => x.WeaponType, WeaponType.Wand)
            .Create();
        // Act
        await sut.AddWeapon(wand);
        var result = await sut.GetWeapon<WandEntity>(x => x.Id == wand.Id);
        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetWeapons_ShouldOnlyReturnListWithCorrectWands()
    {
        // Arrange
        var sut = GetSut();
        
        // Act
        var result = await sut.GetWeapons<WandEntity>();
        
        // Assert
        result.Should().OnlyContain(w => w.WeaponType == WeaponType.Wand);
    }
}