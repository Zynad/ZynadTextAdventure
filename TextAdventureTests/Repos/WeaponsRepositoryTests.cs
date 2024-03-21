using ApplicationServices.Items.Equipment.Weapons;
using AutoFixture;
using Domain.Contexts;
using Domain.Entities.Weapons.Models;
using Domain.Enums;
using Domain.Repos.Weapons;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NSubstitute;

namespace TextAdventureTests.Repos;
public class WeaponsRepositoryTests
{
    private readonly Fixture _autoFixture = new Fixture();
    public WandRepository GetSut()
    {
        var configuration = Substitute.For<IConfiguration>();
        configuration["FilePaths:WeaponsDB"].Returns("C:\\Users\\Zynad\\Desktop\\Adventure\\TextAdventure\\TextAdventure\\Repos\\Weapons\\DB\\WeaponsDBTests.json");
        var context = Substitute.For<DataContext>();
        return new WandRepository(context);
    }

    [Fact]
    public async Task GetWeapon_WithNameBeginnerWand_ShouldReturnCorrectItem()
    {
        // Arrange
        var sut = GetSut();
        var wandName = "Beginner Wand";
        // Act
        var result = await sut.GetAsync(w => w.Name == wandName);
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
            .With(x => x.WeaponType, WeaponTypeEntity.Wand)
            .Create();
        // Act
        await sut.AddAsync(wand);
        var result = await sut.GetAsync(x => x.Id == wand.Id);
        // Assert
        result.Should().NotBeNull();
    }
}