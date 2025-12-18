using Domain.Database;
using Domain.Entities.Weapons.Models;
using Domain.Repos.Weapons;
using Shouldly;
using System.Linq;

namespace TextAdventureTests.Repositories;

public class EquipmentRepositoryTests
{
    private class InMemoryGameDatabase : IGameDatabase
    {
        public DatabaseModel Model { get; private set; } = new();
        public int WriteCount { get; private set; }

        public Task<DatabaseModel> ReadAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Model);
        }

        public Task WriteAsync(DatabaseModel databaseModel, CancellationToken cancellationToken = default)
        {
            Model = databaseModel;
            WriteCount++;
            return Task.CompletedTask;
        }
    }

    private class TestWandRepository(IGameDatabase database) : WandRepository(database);

    [Fact]
    public async Task AddAsync_PersistsEntityToDatabase()
    {
        var database = new InMemoryGameDatabase();
        var repository = new TestWandRepository(database);
        var wand = new WandEntity { Id = Guid.NewGuid(), Name = "Test Wand" };

        var result = await repository.AddAsync(wand);

        result.ShouldBe(wand);
        database.Model.Wands.ShouldContain(w => w.Id == wand.Id && w.Name == wand.Name);
        database.WriteCount.ShouldBe(1);
    }

    [Fact]
    public async Task UpdateAsync_ReplacesMatchingEntity()
    {
        var database = new InMemoryGameDatabase();
        var repository = new TestWandRepository(database);
        var wand = new WandEntity { Id = Guid.NewGuid(), Name = "Original Wand" };
        await repository.AddAsync(wand);

        var updated = new WandEntity { Id = wand.Id, Name = "Updated Wand" };
        await repository.UpdateAsync(updated);

        database.Model.Wands.Count.ShouldBe(1);
        database.Model.Wands.Single().Name.ShouldBe("Updated Wand");
        database.WriteCount.ShouldBe(2);
    }

    [Fact]
    public async Task DeleteAsync_RemovesEntityFromDatabase()
    {
        var database = new InMemoryGameDatabase();
        var repository = new TestWandRepository(database);
        var wand = new WandEntity { Id = Guid.NewGuid(), Name = "Temp Wand" };
        await repository.AddAsync(wand);

        await repository.DeleteAsync(wand);

        database.Model.Wands.ShouldBeEmpty();
        database.WriteCount.ShouldBe(2);
    }
}
