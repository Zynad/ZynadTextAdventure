using ApplicationServices.Authentication;
using ApplicationServices.Contracts.Repositories;
using ApplicationServices.Contracts.Services;
using ApplicationServices.Npc;
using Domain.Core;
using Domain.Entities.Storage;
using Domain.ValueObjects;
using NSubstitute;
using Shouldly;

namespace TextAdventureTests.Npc;

public class NpcInteractionServiceTests
{
    private readonly Guid _accountId = Guid.NewGuid();

    [Fact]
    public async Task Dialogue_interpolates_player_name_and_uses_templates()
    {
        var character = new Character { AccountId = _accountId, Name = "Aerin", Id = Guid.NewGuid() };
        var npc = new TownNpc
        {
            Id = "guard_one",
            Name = "Sentinel", 
            Role = "Guard",
            RoleType = NpcRoleType.Guard,
            Dialogue = new NpcDialogueTemplate
            {
                Greetings = new List<string> { "Halt, {playerName}!" },
                RandomLines = new List<string> { "Stay sharp." }
            }
        };

        var service = CreateService(character, npc, 0);
        var result = await service.GetDialogueAsync("token", character.Id, npc.Id);

        result.Success.ShouldBeTrue();
        result.Payload!.Line.ShouldBe("Halt, Aerin!");
    }

    [Fact]
    public async Task Trade_buy_flow_respects_pricing_and_updates_coins()
    {
        var character = new Character { AccountId = _accountId, Name = "Mara", Id = Guid.NewGuid(), Coins = 12 };
        var npc = new TownNpc { Id = "vendor", Name = "Shopkeep", IsVendor = true, Dialogue = new NpcDialogueTemplate() };

        var vendorService = Substitute.For<IVendorPricingService>();
        vendorService.GetPriceForItemAsync("Town", "potion", Arg.Any<CancellationToken>())
            .Returns(new VendorPrice { ItemId = "potion", BuyPrice = 5m, SellPrice = 2m });

        var service = CreateService(character, npc, 0, vendorService);
        var result = await service.TradeAsync("token", character.Id, npc.Id, "potion", 1, TradeAction.Buy);

        result.Success.ShouldBeTrue();
        result.Payload!.TotalPrice.ShouldBe(5m);
        character.Coins.ShouldBe(7m);
        character.Inventory.Single(i => i.ItemId == "potion").Quantity.ShouldBe(1);
    }

    [Fact]
    public async Task Resolve_action_logs_outcome_and_uses_stats()
    {
        var character = new Character
        {
            AccountId = _accountId,
            Name = "Kara",
            Id = Guid.NewGuid(),
            Stats = new CharacterStats { Combat = 3, Stealth = 1, Pickpocket = 1 }
        };

        var npc = new TownNpc { Id = "trainer", Name = "Drillmaster", Dialogue = new NpcDialogueTemplate() };
        var random = Substitute.For<IRandomService>();
        random.NextInt(1, 21).Returns(18);
        random.NextInt(0, Arg.Any<int>()).Returns(0);

        var service = CreateService(character, npc, 0, randomService: random);
        var result = await service.ResolveActionAsync("token", character.Id, npc.Id, NpcActionType.Combat, 18);

        result.Success.ShouldBeTrue();
        result.Payload!.Success.ShouldBeTrue();
        character.ActionLog.ShouldNotBeEmpty();
        character.ActionLog.Last().Roll.ShouldBeGreaterThanOrEqualTo(21);
    }

    private NpcInteractionService CreateService(
        Character character,
        TownNpc npc,
        int randomChoice,
        IVendorPricingService? vendorPricing = null,
        IRandomService? randomService = null)
    {
        var characterRepo = Substitute.For<ICharacterRepository>();
        characterRepo.GetByIdAsync(character.Id, Arg.Any<CancellationToken>()).Returns(character);
        characterRepo.UpdateAsync(Arg.Any<Character>(), Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);

        var towns = new List<Town> { new() { Name = "Town", Npcs = new List<TownNpc> { npc } } };
        var worldRepo = Substitute.For<IWorldRepository>();
        worldRepo.GetTownsAsync(Arg.Any<CancellationToken>()).Returns(towns);

        var userRepo = Substitute.For<IUserRepository>();
        userRepo.GetByIdAsync(_accountId, Arg.Any<CancellationToken>()).Returns(new UserAccount { Id = _accountId, Username = "tester" });
        var sessionRepo = Substitute.For<ISessionRepository>();
        sessionRepo.GetValidTokenAsync("token", Arg.Any<CancellationToken>()).Returns(new SessionToken
        {
            AccountId = _accountId,
            Token = "token",
            ExpiresAt = DateTimeOffset.UtcNow.AddHours(1)
        });

        var currentUserHandler = new GetCurrentUserHandler(userRepo, sessionRepo);
        var priceSvc = vendorPricing ?? Substitute.For<IVendorPricingService>();

        var rng = randomService ?? Substitute.For<IRandomService>();
        rng.NextInt(Arg.Any<int>(), Arg.Any<int>()).Returns(randomChoice);
        rng.NextDouble().Returns(0.5);
        rng.GetBytes(Arg.Any<int>()).Returns(Array.Empty<byte>());

        return new NpcInteractionService(currentUserHandler, worldRepo, characterRepo, priceSvc, rng);
    }
}
