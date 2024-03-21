using AutoFixture;
using Domain.Contexts;
using Domain.Repos.Weapons;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using System.Linq.Expressions;
using ApplicationServices.Items.Equipment.Weapons;
using ApplicationServices.Items.Equipment.Weapons.BaseWeapons;
using ApplicationServices.Services.Weapons.WeaponServices;
using Domain.Entities.Weapons.Models;
using Domain.Enums;

namespace TextAdventureTests.Services;
public class WeaponsServiceTests
{
    private readonly IWandRepository _mockedRepo = Substitute.For<IWandRepository>();
    private readonly Fixture _autoFixture = new Fixture();
    public WandService GetWandSut(bool useMockedRepo = true)
    {
        if (!useMockedRepo)
        {
            var configuration = Substitute.For<IConfiguration>();
            configuration["FilePaths:WeaponsDB"].Returns("C:\\Users\\Zynad\\Desktop\\Adventure\\TextAdventure\\TextAdventure\\Repos\\Weapons\\DB\\WeaponsDBTests.json");
            var options = new DbContextOptions<DataContext>();
            var context = new DataContext(options);
            var repo = new WandRepository(context);
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
            .With(x => x.WeaponType, WeaponTypeEntity.Wand)
            .CreateMany<WandEntity>()
            .ToList();
        _mockedRepo.GetAsync(Arg.Any<Expression<Func<WandEntity, bool>>>()).Returns(Task.FromResult(wands.First()));
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

    [Fact]
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