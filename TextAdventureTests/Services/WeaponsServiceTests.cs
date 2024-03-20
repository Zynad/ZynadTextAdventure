using AutoFixture;
using FluentAssertions;
using NSubstitute;
using TextAdventure.Items.Equipment.Weapons.BaseWeapons;
using TextAdventure.Repos.Weapons;
namespace TextAdventureTests.Services;
public class WeaponsServiceTests
{
    private readonly IWeaponRepository<Wand> _mockedRepo = Substitute.For<IWeaponRepository<Wand>>();
    private readonly Fixture _autoFixture = new Fixture();
    public WeaponService<Wand> GetSut()
    {
        return new WeaponService<Wand>(_mockedRepo);
    }
    [Fact]
    public async Task GetWeapons_WithThreeItemsInRepo_ShouldReturnListWithItems()
    {
        // Arrange
        var wands = _autoFixture.CreateMany<Wand>().ToList();
        _mockedRepo.GetWeapons().Returns(wands);
        var sut = GetSut();
        // Act
        var result = await sut.GetWeapons();
        // Assert
        result.Count.Should().Be(3);
        result.Should().BeOfType<List<Wand>>();
    }
}