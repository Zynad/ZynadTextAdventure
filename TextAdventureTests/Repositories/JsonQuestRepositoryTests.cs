using ApplicationServices.Contracts.Repositories;
using Domain.Core;
using Domain.Database;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Shouldly;
using TextAdventure.Infrastructure.Configuration;
using TextAdventure.Infrastructure.Repositories;

namespace TextAdventureTests.Repositories;

public class JsonQuestRepositoryTests : IDisposable
{
    private readonly string _tempDirectory;
    private readonly IQuestRepository _repository;

    public JsonQuestRepositoryTests()
    {
        _tempDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(_tempDirectory);

        var options = Options.Create(new DataStoreOptions
        {
            DataDirectory = _tempDirectory,
            QuestsFileName = "quests.json"
        });

        var environment = Substitute.For<IHostEnvironment>();
        environment.ContentRootPath.Returns(_tempDirectory);

        _repository = new JsonQuestRepository(options, environment, NullLogger<JsonQuestRepository>.Instance, new FileConcurrencyProvider());
    }

    [Fact]
    public async Task GetAllAsync_SeedsDefaultQuests()
    {
        var quests = await _repository.GetAllAsync();

        quests.ShouldNotBeEmpty();
        quests.Count.ShouldBe(DbCreateDefault.World.Quests().Count);
    }

    [Fact]
    public async Task UpdateAsync_PersistsQuestChanges()
    {
        var quest = (await _repository.GetAllAsync()).First();
        quest.Description = "Updated description";

        await _repository.UpdateAsync(quest);

        var reloaded = await _repository.GetByIdAsync(quest.Id);
        reloaded.ShouldNotBeNull();
        reloaded!.Description.ShouldBe("Updated description");
    }

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
            // best effort
        }
    }
}
