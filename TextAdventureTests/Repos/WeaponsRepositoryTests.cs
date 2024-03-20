using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NSubstitute;
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
    public async Task GetWands_ReturnsListOfWands()
    {
        // Arrange
        var sut = GetSut();
        
        // Act
        var result = await sut.GetWands();
        
        // Assert
        Assert.IsType<List<WandEntity>>(result);
    }

    [Fact]
    public async Task GetWand_WithNameBeginnerWand_ShouldReturnCorrectItem()
    {
        // Arrange
        var sut = GetSut();
        var wandName = "Beginner Wand";
        // Act
        var result = await sut.GetWand(w => w.Name == wandName);
        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(wandName);
    }
    
    [Fact]
    public async Task AddWand_AddsWandToList()
    {
        // Arrange
        var sut = GetSut();
        var wand = _autoFixture.Create<WandEntity>();
        
        // Act
        await sut.AddWand(wand);
        var result = await sut.GetWands();

        // Assert
        result.Should().ContainEquivalentOf(wand);
    }
}
