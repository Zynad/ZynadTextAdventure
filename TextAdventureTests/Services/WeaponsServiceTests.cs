using AutoFixture;
using FluentAssertions;
using NSubstitute;
using TextAdventure.Items.Equipment.Weapons.BaseWeapons;
using TextAdventure.Repos.Weapons;
using TextAdventure.Repos.Weapons.Models;
using TextAdventure.Services.Weapons;

namespace TextAdventureTests.Services;
public class WeaponsServiceTests
{
    private readonly IWeaponsRepository _mockedRepo = Substitute.For<IWeaponsRepository>();
    private readonly Fixture _autoFixture = new Fixture();
    public WeaponsService GetSut()
    {
        return new WeaponsService(_mockedRepo);
    }

    [Fact]
    public async Task GetWands_WithThreeItemsInRepo_ShouldReturnListWithItems()
    {
        // Arrange
        var wands = _autoFixture.CreateMany<WandEntity>().ToList();
        _mockedRepo.GetWands().Returns(wands);
        var sut = GetSut();

        // Act
        var result = await sut.GetWands();
        
        // Assert
        result.Count.Should().Be(3);
        result.Should().BeOfType<List<Wand>>();
    }
}
