using ApplicationServices.Contracts.Repositories;
using Domain.Core;
using Domain.Entities.Storage;
using Domain.ValueObjects;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TextAdventure.Infrastructure.Configuration;

namespace TextAdventure.Infrastructure.Repositories;

public class JsonWorldRepository : IWorldRepository
{
    private readonly JsonFileStore<WorldState> _store;

    public JsonWorldRepository(IOptions<DataStoreOptions> options, IHostEnvironment environment, ILogger<JsonWorldRepository> logger, FileConcurrencyProvider concurrencyProvider)
    {
        _store = new JsonFileStore<WorldState>(options, environment, logger, concurrencyProvider, options.Value.WorldFileName);
    }

    public async Task<IReadOnlyCollection<Monster>> GetMonstersAsync(CancellationToken cancellationToken = default)
    {
        var world = await ReadWorldAsync(cancellationToken);
        return world.Monsters;
    }

    public async Task<IReadOnlyCollection<CharacterPreset>> GetCharacterPresetsAsync(
        CancellationToken cancellationToken = default)
    {
        var world = await ReadWorldAsync(cancellationToken);
        return world.CharacterPresets;
    }

    public async Task<IReadOnlyCollection<Town>> GetTownsAsync(CancellationToken cancellationToken = default)
    {
        var world = await ReadWorldAsync(cancellationToken);
        return world.Towns;
    }

    public async Task<IReadOnlyCollection<WorldLocationNode>> GetLocationsAsync(CancellationToken cancellationToken = default)
    {
        var world = await ReadWorldAsync(cancellationToken);
        return world.Locations;
    }

    public async Task<IReadOnlyCollection<DropTable>> GetDropTablesAsync(CancellationToken cancellationToken = default)
    {
        var world = await ReadWorldAsync(cancellationToken);
        return world.DropTables;
    }

    public async Task SaveWorldAsync(
        IEnumerable<Town> towns,
        IEnumerable<Monster> monsters,
        IEnumerable<CharacterPreset> characterPresets,
        IEnumerable<WorldLocationNode> locations,
        IEnumerable<DropTable> dropTables,
        CancellationToken cancellationToken = default)
    {
        var world = new WorldState
        {
            Towns = towns.ToList(),
            Monsters = monsters.ToList(),
            CharacterPresets = characterPresets.ToList(),
            Locations = locations.ToList(),
            DropTables = dropTables.ToList()
        };

        await _store.WriteAsync(new[] { world }, cancellationToken);
    }

    private async Task<WorldState> ReadWorldAsync(CancellationToken cancellationToken)
    {
        var data = await _store.ReadAsync(() => new List<WorldState> { CreateDefaultWorldState() }, cancellationToken);
        var world = data.FirstOrDefault() ?? CreateDefaultWorldState();

        world.CharacterPresets ??= new List<CharacterPreset>();
        if (world.CharacterPresets.Count == 0)
        {
            world.CharacterPresets.AddRange(CreateDefaultCharacterPresets());
        }

        world.Monsters ??= new List<Monster>();
        if (world.Monsters.Count == 0)
        {
            world.Monsters.AddRange(CreateDefaultMonsters());
        }

        world.Towns ??= new List<Town>();
        if (world.Towns.Count == 0)
        {
            world.Towns.AddRange(CreateDefaultTowns());
        }

        world.Locations ??= new List<WorldLocationNode>();
        if (world.Locations.Count == 0)
        {
            world.Locations.AddRange(CreateDefaultLocations());
        }

        world.DropTables ??= new List<DropTable>();
        if (world.DropTables.Count == 0)
        {
            world.DropTables.AddRange(CreateDefaultDropTables());
        }

        return world;
    }

    private static WorldState CreateDefaultWorldState()
    {
        return new WorldState
        {
            Towns = CreateDefaultTowns(),
            Monsters = CreateDefaultMonsters(),
            CharacterPresets = CreateDefaultCharacterPresets(),
            Locations = CreateDefaultLocations(),
            DropTables = CreateDefaultDropTables()
        };
    }

    private static List<Monster> CreateDefaultMonsters()
    {
        return new List<Monster>
        {
            new()
            {
                Id = "road_bandit",
                Name = "Roadside Bandit",
                Biome = "Grassland",
                LevelRange = new MonsterStatRange { Min = 1, Max = 3 },
                HitPointRange = new MonsterStatRange { Min = 8, Max = 14 },
                AttackRange = new MonsterStatRange { Min = 2, Max = 4 },
                DefenseRange = new MonsterStatRange { Min = 1, Max = 2 },
                CoinDropRange = new MonsterStatRange { Min = 3, Max = 9 },
                PreferredThreatLevels = new List<string> { "Low" }
            },
            new()
            {
                Id = "wild_boar",
                Name = "Wild Boar",
                Biome = "Grassland",
                LevelRange = new MonsterStatRange { Min = 2, Max = 4 },
                HitPointRange = new MonsterStatRange { Min = 12, Max = 18 },
                AttackRange = new MonsterStatRange { Min = 3, Max = 5 },
                DefenseRange = new MonsterStatRange { Min = 1, Max = 3 },
                CoinDropRange = new MonsterStatRange { Min = 2, Max = 6 },
                PreferredThreatLevels = new List<string> { "Low", "Moderate" }
            },
            new()
            {
                Id = "emberbrook_scout",
                Name = "Wayward Scout",
                Biome = "Village",
                LevelRange = new MonsterStatRange { Min = 3, Max = 5 },
                HitPointRange = new MonsterStatRange { Min = 16, Max = 24 },
                AttackRange = new MonsterStatRange { Min = 4, Max = 6 },
                DefenseRange = new MonsterStatRange { Min = 2, Max = 4 },
                CoinDropRange = new MonsterStatRange { Min = 5, Max = 12 },
                PreferredThreatLevels = new List<string> { "Low", "Moderate" }
            },
            new()
            {
                Id = "cavern_bat",
                Name = "Cavern Bat",
                Biome = "Cave",
                LevelRange = new MonsterStatRange { Min = 2, Max = 4 },
                HitPointRange = new MonsterStatRange { Min = 14, Max = 22 },
                AttackRange = new MonsterStatRange { Min = 4, Max = 6 },
                DefenseRange = new MonsterStatRange { Min = 1, Max = 2 },
                CoinDropRange = new MonsterStatRange { Min = 1, Max = 4 },
                PreferredThreatLevels = new List<string> { "Moderate" }
            },
            new()
            {
                Id = "ruin_skeleton",
                Name = "Restless Skeleton",
                Biome = "Ruins",
                LevelRange = new MonsterStatRange { Min = 4, Max = 7 },
                HitPointRange = new MonsterStatRange { Min = 18, Max = 32 },
                AttackRange = new MonsterStatRange { Min = 5, Max = 8 },
                DefenseRange = new MonsterStatRange { Min = 3, Max = 6 },
                CoinDropRange = new MonsterStatRange { Min = 6, Max = 14 },
                PreferredThreatLevels = new List<string> { "High" }
            },
            new()
            {
                Id = "coastal_siren",
                Name = "Coastal Siren",
                Biome = "Coast",
                LevelRange = new MonsterStatRange { Min = 5, Max = 8 },
                HitPointRange = new MonsterStatRange { Min = 24, Max = 40 },
                AttackRange = new MonsterStatRange { Min = 6, Max = 10 },
                DefenseRange = new MonsterStatRange { Min = 3, Max = 6 },
                CoinDropRange = new MonsterStatRange { Min = 8, Max = 20 },
                PreferredThreatLevels = new List<string> { "Moderate", "High" }
            },
            new()
            {
                Id = "marsh_wisp",
                Name = "Marsh Wisp",
                Biome = "Swamp",
                LevelRange = new MonsterStatRange { Min = 3, Max = 6 },
                HitPointRange = new MonsterStatRange { Min = 16, Max = 28 },
                AttackRange = new MonsterStatRange { Min = 4, Max = 7 },
                DefenseRange = new MonsterStatRange { Min = 2, Max = 5 },
                CoinDropRange = new MonsterStatRange { Min = 5, Max = 12 },
                PreferredThreatLevels = new List<string> { "Moderate" }
            },
            new()
            {
                Id = "mountain_wolf",
                Name = "Highridge Wolf",
                Biome = "Mountain",
                LevelRange = new MonsterStatRange { Min = 4, Max = 8 },
                HitPointRange = new MonsterStatRange { Min = 22, Max = 36 },
                AttackRange = new MonsterStatRange { Min = 6, Max = 9 },
                DefenseRange = new MonsterStatRange { Min = 3, Max = 6 },
                CoinDropRange = new MonsterStatRange { Min = 7, Max = 16 },
                PreferredThreatLevels = new List<string> { "High" }
            },
            new()
            {
                Id = "harbor_cutthroat",
                Name = "Harbor Cutthroat",
                Biome = "Coast",
                LevelRange = new MonsterStatRange { Min = 4, Max = 7 },
                HitPointRange = new MonsterStatRange { Min = 20, Max = 30 },
                AttackRange = new MonsterStatRange { Min = 6, Max = 9 },
                DefenseRange = new MonsterStatRange { Min = 2, Max = 5 },
                CoinDropRange = new MonsterStatRange { Min = 7, Max = 16 },
                PreferredThreatLevels = new List<string> { "Moderate" }
            }
        };
    }

    private static List<Town> CreateDefaultTowns()
    {
        return new List<Town>
        {
            new()
            {
                Name = "Emberbrook",
                VendorInventory = new List<VendorPrice>
                {
                    new() { ItemId = "loaf_of_bread", BuyPrice = 2.0m, SellPrice = 1.0m },
                    new() { ItemId = "whetstone", BuyPrice = 8.0m, SellPrice = 3.0m },
                    new() { ItemId = "leather_cap", BuyPrice = 12.0m, SellPrice = 5.0m },
                    new() { ItemId = "healing_herbs", BuyPrice = 6.0m, SellPrice = 3.0m },
                    new() { ItemId = "minor_healing_potion", BuyPrice = 14.0m, SellPrice = 6.0m }
                },
                Npcs = new List<TownNpc>
                {
                    new() { Id = "emberbrook_mayor", Name = "Mayor Thale", Role = "Mayor", Personality = "Earnest" },
                    new() { Id = "emberbrook_farmer", Name = "Rhea Grainley", Role = "Farmer", Personality = "Cheerful" },
                    new() { Id = "emberbrook_barkeep", Name = "Joren Kask", Role = "Barkeep", Personality = "Wry", IsVendor = true }
                }
            },
            new()
            {
                Name = "Mosslight",
                VendorInventory = new List<VendorPrice>
                {
                    new() { ItemId = "forest_tokens", BuyPrice = 5.0m, SellPrice = 2.0m },
                    new() { ItemId = "travel_rations", BuyPrice = 3.5m, SellPrice = 1.5m },
                    new() { ItemId = "quiver_of_arrows", BuyPrice = 14.0m, SellPrice = 6.0m },
                    new() { ItemId = "healing_herbs", BuyPrice = 6.0m, SellPrice = 3.0m },
                    new() { ItemId = "antidote_phial", BuyPrice = 10.0m, SellPrice = 4.0m }
                },
                Npcs = new List<TownNpc>
                {
                    new() { Id = "mosslight_guard", Name = "Ser Havel", Role = "Guard Captain", Personality = "Stoic" },
                    new() { Id = "mosslight_scavenger", Name = "Fenna Willow", Role = "Forager", Personality = "Curious" },
                    new() { Id = "mosslight_caller", Name = "Brin Bell", Role = "Town Crier", Personality = "Booming" }
                }
            },
            new()
            {
                Name = "Stormwatch Harbor",
                VendorInventory = new List<VendorPrice>
                {
                    new() { ItemId = "salted_fish", BuyPrice = 4.0m, SellPrice = 2.0m },
                    new() { ItemId = "driftwood_charm", BuyPrice = 9.0m, SellPrice = 4.0m },
                    new() { ItemId = "rope_coil", BuyPrice = 7.0m, SellPrice = 3.0m },
                    new() { ItemId = "sailor_cloak", BuyPrice = 15.0m, SellPrice = 7.0m },
                    new() { ItemId = "lesser_mana_potion", BuyPrice = 16.0m, SellPrice = 7.0m }
                },
                Npcs = new List<TownNpc>
                {
                    new() { Id = "stormwatch_dockmaster", Name = "Dockmaster Leira", Role = "Dockmaster", Personality = "Gruff", IsVendor = true },
                    new() { Id = "stormwatch_sailor", Name = "Old Wens", Role = "Sailor", Personality = "Storyteller" },
                    new() { Id = "stormwatch_scrim", Name = "Scrim", Role = "Smuggler", Personality = "Cagey" }
                }
            },
            new()
            {
                Name = "Highridge",
                VendorInventory = new List<VendorPrice>
                {
                    new() { ItemId = "ore_fragment", BuyPrice = 10.0m, SellPrice = 4.0m },
                    new() { ItemId = "sturdy_leather", BuyPrice = 9.0m, SellPrice = 4.0m },
                    new() { ItemId = "iron_ingot", BuyPrice = 18.0m, SellPrice = 8.0m },
                    new() { ItemId = "glowing_crystal", BuyPrice = 22.0m, SellPrice = 10.0m },
                    new() { ItemId = "iron_shield", BuyPrice = 28.0m, SellPrice = 12.0m }
                },
                Npcs = new List<TownNpc>
                {
                    new() { Id = "highridge_miner", Name = "Torun Slate", Role = "Miner", Personality = "Pragmatic" },
                    new() { Id = "highridge_cook", Name = "Elya Pike", Role = "Cook", Personality = "Warm" },
                    new() { Id = "highridge_quartermaster", Name = "Quartermaster Hale", Role = "Quartermaster", Personality = "Exacting", IsVendor = true }
                }
            }
        };
    }

    private static List<DropTable> CreateDefaultDropTables()
    {
        return new List<DropTable>
        {
            new()
            {
                Biome = "Village",
                Drops = new List<string> { "loaf_of_bread", "bundle_of_roots", "healing_herbs", "travel_rations", "minor_healing_potion", "wool_vest" }
            },
            new()
            {
                Biome = "Grassland",
                Drops = new List<string> { "loaf_of_bread", "coin_pouch", "torn_cloth", "field_beans", "rusted_dagger" }
            },
            new()
            {
                Biome = "Forest",
                Drops = new List<string> { "healing_herbs", "tree_sap", "forest_tokens", "stack_of_fungus", "oak_bow" }
            },
            new()
            {
                Biome = "Mountain",
                Drops = new List<string> { "ore_fragment", "sturdy_leather", "coin_pouch", "glowing_crystal", "iron_ingot", "iron_shield" }
            },
            new()
            {
                Biome = "Cave",
                Drops = new List<string> { "glowing_crystal", "bat_wing", "ore_fragment", "crystal_flask", "ashen_staff" }
            },
            new()
            {
                Biome = "Ruins",
                Drops = new List<string> { "ancient_coin", "tattered_map", "mysterious_trinket", "chain_hauberk" }
            },
            new()
            {
                Biome = "Coast",
                Drops = new List<string> { "salted_fish", "coin_pouch", "driftwood_charm", "rope_coil", "sea_blade", "smuggler_cutlass" }
            },
            new()
            {
                Biome = "Swamp",
                Drops = new List<string> { "reedy_bundle", "marsh_pearl", "mossy_fragment", "marsh_hood", "bog_spice" }
            },
            new()
            {
                Biome = "Unknown",
                Drops = new List<string> { "coin_pouch", "mysterious_trinket", "tattered_map", "lesser_mana_potion" }
            }
        };
    }

    private static List<WorldLocationNode> CreateDefaultLocations()
    {
        return new List<WorldLocationNode>
        {
            new()
            {
                Id = "travelers_road",
                Name = "Traveler's Road",
                Description = "A well-worn path where many adventurers begin their journey.",
                Biome = "Grassland",
                ThreatLevel = "Low",
                AdjacentLocationIds = new List<string> { "forked_path", "roadside_inn" }
            },
            new()
            {
                Id = "forked_path",
                Name = "Forked Path",
                Description = "A crossroads lined with signposts pointing toward nearby settlements.",
                Biome = "Grassland",
                ThreatLevel = "Low",
                AdjacentLocationIds = new List<string> { "travelers_road", "emberbrook_gate", "mossy_trail", "roadside_inn" }
            },
            new()
            {
                Id = "roadside_inn",
                Name = "Dusty Rest Inn",
                Description = "A roadside tavern where caravans share news over warm stew.",
                Biome = "Grassland",
                ThreatLevel = "Safe",
                AdjacentLocationIds = new List<string> { "travelers_road", "forked_path" }
            },
            new()
            {
                Id = "emberbrook_gate",
                Name = "Emberbrook Gate",
                Description = "The sturdy wooden gate guarding the village of Emberbrook.",
                Biome = "Village",
                ThreatLevel = "Low",
                AdjacentLocationIds = new List<string> { "forked_path", "emberbrook_square" },
                TownName = "Emberbrook"
            },
            new()
            {
                Id = "emberbrook_square",
                Name = "Emberbrook Square",
                Description = "A bustling square where villagers trade stories and goods.",
                Biome = "Village",
                ThreatLevel = "Safe",
                AdjacentLocationIds = new List<string> { "emberbrook_gate", "emberbrook_inn" },
                TownName = "Emberbrook"
            },
            new()
            {
                Id = "emberbrook_inn",
                Name = "Emberbrook Hearth Inn",
                Description = "A cozy inn with a glowing hearth and plenty of gossip.",
                Biome = "Village",
                ThreatLevel = "Safe",
                AdjacentLocationIds = new List<string> { "emberbrook_square" },
                TownName = "Emberbrook"
            },
            new()
            {
                Id = "mossy_trail",
                Name = "Mossy Trail",
                Description = "A damp trail leading deeper into the trees with soft moss underfoot.",
                Biome = "Grassland",
                ThreatLevel = "Low",
                AdjacentLocationIds = new List<string> { "forked_path", "whispering_woods" }
            },
            new()
            {
                Id = "whispering_woods",
                Name = "Whispering Woods",
                Description = "The wind carries faint whispers through the dense canopy.",
                Biome = "Forest",
                ThreatLevel = "Moderate",
                AdjacentLocationIds = new List<string> { "mossy_trail", "mosslight_green", "hollow_cave" }
            },
            new()
            {
                Id = "mosslight_green",
                Name = "Mosslight Green",
                Description = "A sun-dappled clearing where the villagers of Mosslight gather.",
                Biome = "Village",
                ThreatLevel = "Low",
                AdjacentLocationIds = new List<string> { "whispering_woods", "mosslight_market" },
                TownName = "Mosslight"
            },
            new()
            {
                Id = "mosslight_market",
                Name = "Mosslight Market",
                Description = "Stalls with fragrant herbs and carved trinkets line the path.",
                Biome = "Village",
                ThreatLevel = "Safe",
                AdjacentLocationIds = new List<string> { "mosslight_green", "sagestone_tavern" },
                TownName = "Mosslight"
            },
            new()
            {
                Id = "sagestone_tavern",
                Name = "Sagestone Tavern",
                Description = "Travelers swap rumors over mugs etched with old runes.",
                Biome = "Village",
                ThreatLevel = "Low",
                AdjacentLocationIds = new List<string> { "mosslight_market", "blackmarsh_crossing" }
            },
            new()
            {
                Id = "hollow_cave",
                Name = "Hollow Cave",
                Description = "A yawning cave mouth breathing cool, damp air.",
                Biome = "Cave",
                ThreatLevel = "Moderate",
                AdjacentLocationIds = new List<string> { "whispering_woods", "sunless_depths" }
            },
            new()
            {
                Id = "sunless_depths",
                Name = "Sunless Depths",
                Description = "The cave descends into a maze of echoing tunnels.",
                Biome = "Cave",
                ThreatLevel = "High",
                AdjacentLocationIds = new List<string> { "hollow_cave", "broken_keep" }
            },
            new()
            {
                Id = "broken_keep",
                Name = "Broken Keep",
                Description = "Ruined battlements crumble over a forgotten cellar.",
                Biome = "Ruins",
                ThreatLevel = "High",
                AdjacentLocationIds = new List<string> { "sunless_depths", "blackmarsh_crossing" }
            },
            new()
            {
                Id = "blackmarsh_crossing",
                Name = "Blackmarsh Crossing",
                Description = "Soggy boardwalks stretch over the dark marsh water.",
                Biome = "Swamp",
                ThreatLevel = "Moderate",
                AdjacentLocationIds = new List<string> { "sagestone_tavern", "broken_keep", "coastal_road" }
            },
            new()
            {
                Id = "coastal_road",
                Name = "Coastal Road",
                Description = "A sea-sprayed road dotted with fisher carts and gulls.",
                Biome = "Coast",
                ThreatLevel = "Low",
                AdjacentLocationIds = new List<string> { "blackmarsh_crossing", "stormwatch_gate", "old_watchtower" }
            },
            new()
            {
                Id = "stormwatch_gate",
                Name = "Stormwatch Gate",
                Description = "Weathered stone arches lead into the harbor city.",
                Biome = "Coast",
                ThreatLevel = "Low",
                AdjacentLocationIds = new List<string> { "coastal_road", "stormwatch_market", "stormwatch_docks" },
                TownName = "Stormwatch Harbor"
            },
            new()
            {
                Id = "stormwatch_market",
                Name = "Stormwatch Market",
                Description = "Vendors call out prices for fresh catches and exotic spices.",
                Biome = "Coast",
                ThreatLevel = "Safe",
                AdjacentLocationIds = new List<string> { "stormwatch_gate" },
                TownName = "Stormwatch Harbor"
            },
            new()
            {
                Id = "stormwatch_docks",
                Name = "Stormwatch Docks",
                Description = "Ships bob in the harbor beside nets heavy with fish.",
                Biome = "Coast",
                ThreatLevel = "Low",
                AdjacentLocationIds = new List<string> { "stormwatch_gate" },
                TownName = "Stormwatch Harbor"
            },
            new()
            {
                Id = "old_watchtower",
                Name = "Old Watchtower",
                Description = "A leaning tower that still offers a sweeping coastal view.",
                Biome = "Ruins",
                ThreatLevel = "Moderate",
                AdjacentLocationIds = new List<string> { "coastal_road", "highridge_pass" }
            },
            new()
            {
                Id = "highridge_pass",
                Name = "Highridge Pass",
                Description = "A narrow pass winding upward through jagged cliffs.",
                Biome = "Mountain",
                ThreatLevel = "High",
                AdjacentLocationIds = new List<string> { "old_watchtower", "highridge_plaza" }
            },
            new()
            {
                Id = "highridge_plaza",
                Name = "Highridge Plaza",
                Description = "The mountain town's central square overlooking the valley.",
                Biome = "Mountain",
                ThreatLevel = "Moderate",
                AdjacentLocationIds = new List<string> { "highridge_pass", "highridge_forge" },
                TownName = "Highridge"
            },
            new()
            {
                Id = "highridge_forge",
                Name = "Highridge Forge",
                Description = "Forgefires light the snow around the mountain smithy.",
                Biome = "Mountain",
                ThreatLevel = "Moderate",
                AdjacentLocationIds = new List<string> { "highridge_plaza" },
                TownName = "Highridge"
            }
        };
    }

    private static List<CharacterPreset> CreateDefaultCharacterPresets()
    {
        return new List<CharacterPreset>
        {
            new()
            {
                Id = "warrior",
                Name = "Warrior",
                Description = "A seasoned fighter with dependable gear.",
                StartingLocation = WorldLocation.Default(),
                StartingInventory = new List<InventoryItem>
                {
                    new() { ItemId = "rusty_sword", Quantity = 1 },
                    new() { ItemId = "worn_shield", Quantity = 1 },
                    new() { ItemId = "loaf_of_bread", Quantity = 3 }
                }
            },
            new()
            {
                Id = "ranger",
                Name = "Ranger",
                Description = "A nimble hunter who travels light and strikes from range.",
                StartingLocation = WorldLocation.Default(),
                StartingInventory = new List<InventoryItem>
                {
                    new() { ItemId = "shortbow", Quantity = 1 },
                    new() { ItemId = "quiver_of_arrows", Quantity = 20 },
                    new() { ItemId = "traveler_cloak", Quantity = 1 }
                }
            },
            new()
            {
                Id = "mystic",
                Name = "Mystic",
                Description = "A student of the arcane starting their journey with basic focus tools.",
                StartingLocation = WorldLocation.Default(),
                StartingInventory = new List<InventoryItem>
                {
                    new() { ItemId = "oak_staff", Quantity = 1 },
                    new() { ItemId = "apprentice_robes", Quantity = 1 },
                    new() { ItemId = "healing_herbs", Quantity = 2 }
                }
            }
        };
    }
}
