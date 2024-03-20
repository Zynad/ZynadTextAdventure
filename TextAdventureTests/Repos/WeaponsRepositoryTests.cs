using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using TextAdventure.Items.Equipment.Weapons;
using TextAdventure.Repos.Weapons.Models;
using TextAdventure.Repos.Weapons.WeaponRepos;

namespace TextAdventureTests.Repos;
public class WeaponsRepositoryTests
{
    private readonly Fixture _autoFixture = new Fixture();
    public WandRepository GetWandSut()
    {
        var configuration = Substitute.For<IConfiguration>();
        configuration["FilePaths:WeaponsDB"].Returns("C:\\Users\\Zynad\\OneDrive\\Kul Kod\\Adventure\\TextAdventure\\TextAdventure\\Repos\\Weapons\\DB\\WeaponsDBTests.json");
        return new WandRepository(configuration);
    }

    [Fact]
    public async Task GetWeapon_WithNameBeginnerWand_ShouldReturnCorrectItem()
    {
        // Arrange
        var sut = GetWandSut();
        var wandName = "Beginner Wand";
        // Act
        var result = await sut.GetWeapon(w => w.Name == wandName);
        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(wandName);
    }
    [Fact]
    public async Task AddWeapon_AddsWandToList()
    {
        // Arrange
        var sut = GetWandSut();
        var wand = _autoFixture.Create<WandEntity>();
        // Act
        await sut.AddWeapon(wand);
        var result = await sut.GetWeapons();
        // Assert
        result.Should().ContainEquivalentOf(wand);
    }

    [Fact]
    public async Task GetWeapons_ShouldOnlyReturnListWithCorrectWands()
    {
        // Arrange
        var sut = GetWandSut();
        
        // Act
        var result = await sut.GetWeapons();
        
        // Assert
        result.Should().OnlyContain(w => w.WeaponType == WeaponType.Wand);
    }
}