using System.Linq.Expressions;
using ApplicationServices.Classes;
using ApplicationServices.Items.Equipment.Armor;
using ApplicationServices.Items.Equipment.Armor.BaseArmors;
using ApplicationServices.Items.Equipment.Weapons;
using ApplicationServices.Items.Equipment.Weapons.BaseWeapons;
using ApplicationServices.PlayerSettings;
using ApplicationServices.Services.Armor;
using ApplicationServices.Services.Weapons.WeaponServices;
using Domain.Entities.Armor.Models;
using Domain.Entities.Weapons.Models;
using NSubstitute;
using Shouldly;

namespace TextAdventureTests.Adventure;

public class VocationLoadoutTests
{
    [Fact]
    public async Task Knight_SetBaseValues_AssignsDefaultLoadout()
    {
        var axeService = Substitute.For<IAxeService>();
        var swordService = Substitute.For<ISwordService>();
        var bootsService = Substitute.For<IBootsService>();
        var chestService = Substitute.For<IChestService>();
        var glovesService = Substitute.For<IGlovesService>();
        var helmetService = Substitute.For<IHelmetService>();
        var legsService = Substitute.For<ILegsService>();

        var sword = new Sword { Name = "Iron Shortsword" };
        var boots = new Boots { Name = "Bronze Sabatons" };
        var chest = new Chest { Name = "Bronze Cuirass" };
        var gloves = new Gloves { Name = "Steel Gauntlets" };
        var helmet = new Helmet { Name = "Iron Guard Helmet" };
        var legs = new Legs { Name = "Bronze Greaves" };

        swordService
            .GetWeapon(Arg.Is<Expression<Func<SwordEntity, bool>>>(expr => expr.Compile().Invoke(new SwordEntity { Name = sword.Name })))
            .Returns(Task.FromResult(sword));
        bootsService
            .GetBoots(Arg.Is<Expression<Func<BootsEntity, bool>>>(expr => expr.Compile().Invoke(new BootsEntity { Name = boots.Name })))
            .Returns(Task.FromResult(boots));
        chestService
            .GetChest(Arg.Is<Expression<Func<ChestEntity, bool>>>(expr => expr.Compile().Invoke(new ChestEntity { Name = chest.Name })))
            .Returns(Task.FromResult(chest));
        glovesService
            .GetGlove(Arg.Is<Expression<Func<GlovesEntity, bool>>>(expr => expr.Compile().Invoke(new GlovesEntity { Name = gloves.Name })))
            .Returns(Task.FromResult(gloves));
        helmetService
            .GetHelmet(Arg.Is<Expression<Func<HelmetEntity, bool>>>(expr => expr.Compile().Invoke(new HelmetEntity { Name = helmet.Name })))
            .Returns(Task.FromResult(helmet));
        legsService
            .GetLeg(Arg.Is<Expression<Func<LegsEntity, bool>>>(expr => expr.Compile().Invoke(new LegsEntity { Name = legs.Name })))
            .Returns(Task.FromResult(legs));

        var knight = new Knight(axeService, swordService, bootsService, chestService, glovesService, helmetService, legsService);
        var player = new Player();

        await knight.SetBaseValues(player);

        player.HitPoints.ShouldBe(60);
        player.MainHand.ShouldBeSameAs(sword);
        player.Boots.ShouldBeSameAs(boots);
        player.Chest.ShouldBeSameAs(chest);
        player.Gloves.ShouldBeSameAs(gloves);
        player.Helmet.ShouldBeSameAs(helmet);
        player.Legs.ShouldBeSameAs(legs);
        player.CanCarryArmorType.ShouldBe(new List<ArmorMaterial> { ArmorMaterial.Plate, ArmorMaterial.Scale, ArmorMaterial.Chain });
        player.CanCarryWeaponType.ShouldBe(new List<WeaponType> { WeaponType.Sword, WeaponType.Axe });
    }

    [Fact]
    public async Task Mage_SetBaseValues_AssignsDefaultLoadout()
    {
        var wandService = Substitute.For<IWandService>();
        var staffService = Substitute.For<IStaffService>();
        var bootsService = Substitute.For<IBootsService>();
        var chestService = Substitute.For<IChestService>();
        var glovesService = Substitute.For<IGlovesService>();
        var helmetService = Substitute.For<IHelmetService>();
        var legsService = Substitute.For<ILegsService>();

        var staff = new Staff { Name = "Oak Quarterstaff" };
        var boots = new Boots { Name = "Mystic Sandals" };
        var chest = new Chest { Name = "Mystic Robes" };
        var gloves = new Gloves { Name = "Battlemage Wraps" };
        var helmet = new Helmet { Name = "Mystic Circlet" };
        var legs = new Legs { Name = "Mystic Legwraps" };

        staffService
            .GetWeapon(Arg.Is<Expression<Func<StaffEntity, bool>>>(expr => expr.Compile().Invoke(new StaffEntity { Name = staff.Name })))
            .Returns(Task.FromResult(staff));
        bootsService
            .GetBoots(Arg.Is<Expression<Func<BootsEntity, bool>>>(expr => expr.Compile().Invoke(new BootsEntity { Name = boots.Name })))
            .Returns(Task.FromResult(boots));
        chestService
            .GetChest(Arg.Is<Expression<Func<ChestEntity, bool>>>(expr => expr.Compile().Invoke(new ChestEntity { Name = chest.Name })))
            .Returns(Task.FromResult(chest));
        glovesService
            .GetGlove(Arg.Is<Expression<Func<GlovesEntity, bool>>>(expr => expr.Compile().Invoke(new GlovesEntity { Name = gloves.Name })))
            .Returns(Task.FromResult(gloves));
        helmetService
            .GetHelmet(Arg.Is<Expression<Func<HelmetEntity, bool>>>(expr => expr.Compile().Invoke(new HelmetEntity { Name = helmet.Name })))
            .Returns(Task.FromResult(helmet));
        legsService
            .GetLeg(Arg.Is<Expression<Func<LegsEntity, bool>>>(expr => expr.Compile().Invoke(new LegsEntity { Name = legs.Name })))
            .Returns(Task.FromResult(legs));

        var mage = new Mage(wandService, staffService, bootsService, chestService, glovesService, helmetService, legsService);
        var player = new Player();

        await mage.SetBaseValues(player);

        player.HitPoints.ShouldBe(30);
        player.MainHand.ShouldBeSameAs(staff);
        player.Boots.ShouldBeSameAs(boots);
        player.Chest.ShouldBeSameAs(chest);
        player.Gloves.ShouldBeSameAs(gloves);
        player.Helmet.ShouldBeSameAs(helmet);
        player.Legs.ShouldBeSameAs(legs);
        player.CanCarryArmorType.ShouldBe(new List<ArmorMaterial> { ArmorMaterial.Cloth });
        player.CanCarryWeaponType.ShouldBe(new List<WeaponType> { WeaponType.Staff, WeaponType.Wand });
    }
}
