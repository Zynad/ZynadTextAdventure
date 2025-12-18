using Domain.Database;
using Domain.Entities.Armor.Models;
using Domain.Entities.Items.Models;
using Domain.Entities.Weapons.Models;
using Domain.Enums;
using Domain.Repos;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Shouldly;
using TextAdventure.Infrastructure.Database;
using TextAdventure.Infrastructure.Repositories.Armor;
using TextAdventure.Infrastructure.Repositories.Weapons;

namespace TextAdventureTests.Repositories;

public class JsonEquipmentRepositoryTests : IDisposable
{
    private readonly string _tempDirectory;
    private readonly JsonDatabase _database;

    public JsonEquipmentRepositoryTests()
    {
        _tempDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(_tempDirectory);

        var options = Options.Create(new JsonDatabaseOptions
        {
            DatabasePath = Path.Combine(_tempDirectory, "database.json")
        });

        var environment = Substitute.For<IHostEnvironment>();
        environment.ContentRootPath.Returns(_tempDirectory);

        _database = new JsonDatabase(options, NullLogger<JsonDatabase>.Instance, environment);
    }

    [Fact]
    public async Task HelmetRepository_PerformsCrudAgainstDatabase()
    {
        var repository = new JsonHelmetRepository(_database);
        var entity = CreateHelmet();

        await VerifyCrudAsync(repository, entity, e => e.Name = "Updated Helmet", model => model.Helmets);
    }

    [Fact]
    public async Task ChestRepository_PerformsCrudAgainstDatabase()
    {
        var repository = new JsonChestRepository(_database);
        var entity = CreateChest();

        await VerifyCrudAsync(repository, entity, e => e.Name = "Updated Chest", model => model.Chests);
    }

    [Fact]
    public async Task GlovesRepository_PerformsCrudAgainstDatabase()
    {
        var repository = new JsonGlovesRepository(_database);
        var entity = CreateGloves();

        await VerifyCrudAsync(repository, entity, e => e.Name = "Updated Gloves", model => model.Gloves);
    }

    [Fact]
    public async Task LegsRepository_PerformsCrudAgainstDatabase()
    {
        var repository = new JsonLegsRepository(_database);
        var entity = CreateLegs();

        await VerifyCrudAsync(repository, entity, e => e.Name = "Updated Legs", model => model.Legs);
    }

    [Fact]
    public async Task BootsRepository_PerformsCrudAgainstDatabase()
    {
        var repository = new JsonBootsRepository(_database);
        var entity = CreateBoots();

        await VerifyCrudAsync(repository, entity, e => e.Name = "Updated Boots", model => model.Boots);
    }

    [Fact]
    public async Task SwordRepository_PerformsCrudAgainstDatabase()
    {
        var repository = new JsonSwordRepository(_database);
        var entity = CreateSword();

        await VerifyCrudAsync(repository, entity, e => e.Name = "Updated Sword", model => model.Swords);
    }

    [Fact]
    public async Task AxeRepository_PerformsCrudAgainstDatabase()
    {
        var repository = new JsonAxeRepository(_database);
        var entity = CreateAxe();

        await VerifyCrudAsync(repository, entity, e => e.Name = "Updated Axe", model => model.Axes);
    }

    [Fact]
    public async Task WandRepository_PerformsCrudAgainstDatabase()
    {
        var repository = new JsonWandRepository(_database);
        var entity = CreateWand();

        await VerifyCrudAsync(repository, entity, e => e.Name = "Updated Wand", model => model.Wands);
    }

    [Fact]
    public async Task StaffRepository_PerformsCrudAgainstDatabase()
    {
        var repository = new JsonStaffRepository(_database);
        var entity = CreateStaff();

        await VerifyCrudAsync(repository, entity, e => e.Name = "Updated Staff", model => model.Staff);
    }

    private async Task VerifyCrudAsync<TEntity>(IBaseRepo<TEntity> repository, TEntity entity, Action<TEntity> mutate, Func<DatabaseModel, IEnumerable<TEntity>> setAccessor)
        where TEntity : ItemsBaseEntity
    {
        await repository.AddAsync(entity);

        var fetched = await repository.GetAsync(e => e.Id == entity.Id);
        fetched.ShouldNotBeNull();
        fetched.Id.ShouldBe(entity.Id);

        mutate(entity);
        await repository.UpdateAsync(entity);

        var updated = await repository.GetAsync(e => e.Id == entity.Id);
        updated.Name.ShouldBe(entity.Name);

        var databaseAfterUpdate = await _database.ReadAsync();
        setAccessor(databaseAfterUpdate).ShouldContain(e => e.Id == entity.Id && e.Name == entity.Name);

        await repository.DeleteAsync(entity);

        var deleted = await repository.GetAsync(e => e.Id == entity.Id);
        deleted.ShouldBeNull();

        var databaseAfterDelete = await _database.ReadAsync();
        setAccessor(databaseAfterDelete).ShouldNotContain(e => e.Id == entity.Id);
    }

    private static HelmetEntity CreateHelmet() => new()
    {
        Id = Guid.NewGuid(),
        Name = "Test Helmet",
        LevelRequirement = 1,
        Rarity = RarityEntity.Common,
        Value = 10,
        Weight = 2,
        Durability = 20,
        Material = ArmorMaterialEntity.Leather,
        PhysicalDefense = 2,
        MagicResistance = 1
    };

    private static ChestEntity CreateChest() => new()
    {
        Id = Guid.NewGuid(),
        Name = "Test Chest",
        LevelRequirement = 1,
        Rarity = RarityEntity.Common,
        Value = 12,
        Weight = 5,
        Durability = 25,
        Material = ArmorMaterialEntity.Plate,
        PhysicalDefense = 3,
        MagicResistance = 1
    };

    private static GlovesEntity CreateGloves() => new()
    {
        Id = Guid.NewGuid(),
        Name = "Test Gloves",
        LevelRequirement = 1,
        Rarity = RarityEntity.Common,
        Value = 8,
        Weight = 1,
        Durability = 15,
        Material = ArmorMaterialEntity.Leather,
        PhysicalDefense = 1,
        MagicResistance = 1
    };

    private static LegsEntity CreateLegs() => new()
    {
        Id = Guid.NewGuid(),
        Name = "Test Legs",
        LevelRequirement = 1,
        Rarity = RarityEntity.Common,
        Value = 11,
        Weight = 4,
        Durability = 22,
        Material = ArmorMaterialEntity.Scale,
        PhysicalDefense = 3,
        MagicResistance = 1
    };

    private static BootsEntity CreateBoots() => new()
    {
        Id = Guid.NewGuid(),
        Name = "Test Boots",
        LevelRequirement = 1,
        Rarity = RarityEntity.Common,
        Value = 9,
        Weight = 2,
        Durability = 18,
        Material = ArmorMaterialEntity.Leather,
        PhysicalDefense = 2,
        MagicResistance = 1
    };

    private static SwordEntity CreateSword() => new()
    {
        Id = Guid.NewGuid(),
        Name = "Test Sword",
        LevelRequirement = 1,
        Rarity = RarityEntity.Common,
        Value = 20,
        Weight = 5,
        Durability = 30,
        Material = WeaponMaterialEntity.Steel,
        WeaponType = WeaponTypeEntity.Sword,
        MeleeAttackValue = 5,
        RangedAttackValue = 0,
        MagicAttackValue = 0,
        IsRanged = false,
        TwoHanded = false,
        Range = 1,
        MagicPower = 0
    };

    private static AxeEntity CreateAxe() => new()
    {
        Id = Guid.NewGuid(),
        Name = "Test Axe",
        LevelRequirement = 1,
        Rarity = RarityEntity.Common,
        Value = 18,
        Weight = 6,
        Durability = 28,
        Material = WeaponMaterialEntity.Iron,
        WeaponType = WeaponTypeEntity.Axe,
        MeleeAttackValue = 6,
        RangedAttackValue = 0,
        MagicAttackValue = 0,
        IsRanged = false,
        TwoHanded = true,
        Range = 1,
        MagicPower = 0
    };

    private static WandEntity CreateWand() => new()
    {
        Id = Guid.NewGuid(),
        Name = "Test Wand",
        LevelRequirement = 1,
        Rarity = RarityEntity.Common,
        Value = 16,
        Weight = 1,
        Durability = 24,
        Material = WeaponMaterialEntity.Wood,
        WeaponType = WeaponTypeEntity.Wand,
        MeleeAttackValue = 1,
        RangedAttackValue = 2,
        MagicAttackValue = 4,
        IsRanged = true,
        TwoHanded = false,
        Range = 5,
        MagicPower = 3
    };

    private static StaffEntity CreateStaff() => new()
    {
        Id = Guid.NewGuid(),
        Name = "Test Staff",
        LevelRequirement = 1,
        Rarity = RarityEntity.Common,
        Value = 22,
        Weight = 4,
        Durability = 26,
        Material = WeaponMaterialEntity.Wood,
        WeaponType = WeaponTypeEntity.Staff,
        MeleeAttackValue = 2,
        RangedAttackValue = 3,
        MagicAttackValue = 5,
        IsRanged = true,
        TwoHanded = true,
        Range = 6,
        MagicPower = 4
    };

    public void Dispose()
    {
        try
        {
            if (Directory.Exists(_tempDirectory))
            {
                Directory.Delete(_tempDirectory, true);
            }
        }
        catch
        {
            // best effort cleanup
        }
    }
}
