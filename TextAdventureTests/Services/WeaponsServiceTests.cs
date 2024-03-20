using System.Configuration;
using System.Runtime.CompilerServices;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using TextAdventure.Items.Equipment.Weapons;
using TextAdventure.Items.Equipment.Weapons.BaseWeapons;
using TextAdventure.Repos.Weapons;
using TextAdventure.Repos.Weapons.Models;
using TextAdventure.Services.Weapons.WeaponServices;

namespace TextAdventureTests.Services;
public class WeaponsServiceTests
{
    private readonly IWeaponsRepository _mockedRepo = Substitute.For<IWeaponsRepository>();
    private readonly Fixture _autoFixture = new Fixture();
    public WandService GetWandSut(bool useMockedRepo = true)
    {
        if (!useMockedRepo)
        {
            var configuration = Substitute.For<IConfiguration>();
            configuration["FilePaths:WeaponsDB"].Returns("C:\\Users\\Zynad\\OneDrive\\Kul Kod\\Adventure\\TextAdventure\\TextAdventure\\Repos\\Weapons\\DB\\WeaponsDBTests.json");
            var repo = new WeaponsRepository(configuration);
            return new WandService(repo);
        }
        return new WandService(_mockedRepo);
    }

    [Fact]
    public async Task GetWeapons_WithThreeWandsInRepo_ShouldReturnListWithItems()
    {
        // Arrange
        var wands = _autoFixture
            .Build<WandEntity>()
            .With(x => x.WeaponType, WeaponType.Wand)
            .CreateMany<WandEntity>()
            .ToList();
        _mockedRepo.GetWeapons<WandEntity>(Arg.Any<WeaponType>()).Returns(wands);
        var sut = GetWandSut();
        // Act
        var result = await sut.GetWeapons();
        // Assert
        result.Count.Should().Be(3);
        result.OfType<Wand>().Count().Should().Be(3);
    }

    [Fact(Skip = "Only run this test manually.")]
    public async Task GetWeapons_WithRealRepo_ShouldReturnListWithItems()
    {
        // Arrange
        var sut = GetWandSut(false);
        // Act
        var result = await sut.GetWeapons();
        // Assert
        result.Count.Should().Be(2);
        result.OfType<Wand>().Count().Should().Be(2);
    }

    [Fact(Skip = "Only run this test manually.")]
    public async Task GetWeapon_WithRealRepo_ShouldReturnCorrectItem()
    {
        // Arrange
        var sut = GetWandSut(false);
        // Act
        var result = await sut.GetWeapon(x => x.Name == "Beginner Wand");
        // Assert
        result.Should().NotBeNull();
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