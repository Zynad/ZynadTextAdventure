using AutoFixture;
using FluentAssertions;
using NSubstitute;
using TextAdventure.Items.Equipment.Weapons.BaseWeapons;
using TextAdventure.Repos.Weapons.Models;
using TextAdventure.Repos.Weapons.WeaponRepos;
using TextAdventure.Services.Weapons.WeaponServices;

namespace TextAdventureTests.Services;
public class WeaponsServiceTests
{
    private readonly IWandRepository _mockedRepo = Substitute.For<IWandRepository>();
    private readonly Fixture _autoFixture = new Fixture();
    public WandService GetWandSut()
    {
        return new WandService(_mockedRepo);
    }

    [Fact]
    public async Task GetWeapons_WithThreeWandsInRepo_ShouldReturnListWithItems()
    {
        // Arrange
        var wands = _autoFixture.CreateMany<WandEntity>().ToList();
        _mockedRepo.GetWeapons().Returns(wands);
        var sut = GetWandSut();
        // Act
        var result = await sut.GetWeapons();
        // Assert
        result.Count.Should().Be(3);
        result.OfType<Wand>().Count().Should().Be(3);
    }
    
    //[Fact]
    //public async Task GetWeapons_WithThreeStavesInRepo_ShouldReturnListWithItems()
    //{
    //    // Arrange
    //    var staves = _autoFixture.CreateMany<StaffEntity>().Cast<WeaponBaseEntity>().ToList();
    //    _mockedRepo.GetWeapons().Returns(Task.FromResult(staves));
    //    var sut = GetSut();
    //    // Act
    //    var result = await sut.GetWeapons();
    //    var staffResult = result.Cast<Staff>().ToList();

    //    // Assert
    //    staffResult.Count.Should().Be(3);
    //    staffResult.OfType<Staff>().Count().Should().Be(3);
    //}


}