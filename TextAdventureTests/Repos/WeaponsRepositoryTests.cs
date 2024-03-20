using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using TextAdventure.Items.Equipment.Weapons.BaseWeapons;
using TextAdventure.Repos.Weapons;
using TextAdventure.Repos.Weapons.Models;
namespace TextAdventureTests.Repos;
public class WeaponsRepositoryTests
{
    private readonly Fixture _autoFixture = new Fixture();
    public WeaponRepository<Wand> GetSut()
    {
        var configuration = Substitute.For<IConfiguration>();
        configuration["FilePaths:WeaponsDB"].Returns("C:\\Users\\Zynad\\OneDrive\\Kul Kod\\Adventure\\TextAdventure\\TextAdventure\\Repos\\Weapons\\DB\\WeaponsDBTests.json");
        return new WeaponRepository<Wand>(configuration);
    }
    [Fact]
    public async Task GetWeapons_ReturnsListOfWands()
    {
        // Arrange
        var sut = GetSut();
        // Act
        var result = await sut.GetWeapons();
        // Assert
        Assert.IsType<List<Wand>>(result);
    }
    [Fact]
    public async Task GetWeapon_WithNameBeginnerWand_ShouldReturnCorrectItem()
    {
        // Arrange
        var sut = GetSut();
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
        var sut = GetSut();
        var wand = _autoFixture.Create<Wand>();
        // Act
        await sut.AddWeapon(wand);
        var result = await sut.GetWeapons();
        // Assert
        result.Should().ContainEquivalentOf(wand);
    }
}