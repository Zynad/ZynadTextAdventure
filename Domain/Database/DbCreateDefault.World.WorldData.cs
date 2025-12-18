using Domain.Core;
using Domain.ValueObjects;

namespace Domain.Database;

public static partial class DbCreateDefault
{
    public static partial class World
    {
        public static List<Monster> Monsters()
        {
            return
            [
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
                    PreferredThreatLevels = ["Low"]
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
                    PreferredThreatLevels = ["Low", "Moderate"]
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
                    PreferredThreatLevels = ["Low", "Moderate"]
                },

                new()
                {
                    Id = "forest_sprite",
                    Name = "Forest Sprite",
                    Biome = "Forest",
                    LevelRange = new MonsterStatRange { Min = 2, Max = 4 },
                    HitPointRange = new MonsterStatRange { Min = 10, Max = 18 },
                    AttackRange = new MonsterStatRange { Min = 3, Max = 6 },
                    DefenseRange = new MonsterStatRange { Min = 1, Max = 3 },
                    CoinDropRange = new MonsterStatRange { Min = 4, Max = 10 },
                    PreferredThreatLevels = ["Low", "Moderate"]
                },

                new()
                {
                    Id = "rune_tortoise",
                    Name = "Rune-Touched Tortoise",
                    Biome = "Forest",
                    LevelRange = new MonsterStatRange { Min = 3, Max = 6 },
                    HitPointRange = new MonsterStatRange { Min = 18, Max = 30 },
                    AttackRange = new MonsterStatRange { Min = 3, Max = 7 },
                    DefenseRange = new MonsterStatRange { Min = 4, Max = 8 },
                    CoinDropRange = new MonsterStatRange { Min = 5, Max = 14 },
                    PreferredThreatLevels = ["Moderate"]
                },

                new()
                {
                    Id = "cave_stalker",
                    Name = "Cave Stalker",
                    Biome = "Cave",
                    LevelRange = new MonsterStatRange { Min = 3, Max = 6 },
                    HitPointRange = new MonsterStatRange { Min = 16, Max = 28 },
                    AttackRange = new MonsterStatRange { Min = 4, Max = 8 },
                    DefenseRange = new MonsterStatRange { Min = 2, Max = 5 },
                    CoinDropRange = new MonsterStatRange { Min = 6, Max = 15 },
                    PreferredThreatLevels = ["Moderate"]
                },

                new()
                {
                    Id = "crystal_spider",
                    Name = "Crystal Spider",
                    Biome = "Cave",
                    LevelRange = new MonsterStatRange { Min = 4, Max = 7 },
                    HitPointRange = new MonsterStatRange { Min = 20, Max = 32 },
                    AttackRange = new MonsterStatRange { Min = 5, Max = 9 },
                    DefenseRange = new MonsterStatRange { Min = 3, Max = 6 },
                    CoinDropRange = new MonsterStatRange { Min = 7, Max = 16 },
                    PreferredThreatLevels = ["Moderate", "High"]
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
                    PreferredThreatLevels = ["High"]
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
                    PreferredThreatLevels = ["Moderate"]
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
                    PreferredThreatLevels = ["Moderate"]
                }
            ];
        }

        public static List<CharacterPreset> CharacterPresets()
        {
            return
            [
                new()
                {
                    Id = "warrior",
                    Name = "Warrior",
                    Description = "A seasoned fighter with dependable gear.",
                    StartingLocation = WorldLocation.Default(),
                    StartingInventory =
                    [
                        new() { ItemId = "rusty_sword", Quantity = 1 },
                        new() { ItemId = "worn_shield", Quantity = 1 },
                        new() { ItemId = "loaf_of_bread", Quantity = 3 }
                    ]
                },

                new()
                {
                    Id = "ranger",
                    Name = "Ranger",
                    Description = "A nimble hunter who travels light and strikes from range.",
                    StartingLocation = WorldLocation.Default(),
                    StartingInventory =
                    [
                        new() { ItemId = "shortbow", Quantity = 1 },
                        new() { ItemId = "quiver_of_arrows", Quantity = 20 },
                        new() { ItemId = "traveler_cloak", Quantity = 1 }
                    ]
                },

                new()
                {
                    Id = "mystic",
                    Name = "Mystic",
                    Description = "A student of the arcane starting their journey with basic focus tools.",
                    StartingLocation = WorldLocation.Default(),
                    StartingInventory =
                    [
                        new() { ItemId = "oak_staff", Quantity = 1 },
                        new() { ItemId = "apprentice_robes", Quantity = 1 },
                        new() { ItemId = "healing_herbs", Quantity = 2 }
                    ]
                }
            ];
        }

        public static List<WorldLocationNode> Locations()
        {
            return
            [
                new()
                {
                    Id = "travelers_road",
                    Name = "Traveler's Road",
                    Description = "A well-worn path where many adventurers begin their journey.",
                    Biome = "Grassland",
                    ThreatLevel = "Low",
                    AdjacentLocationIds = ["forked_path", "roadside_inn"]
                },

                new()
                {
                    Id = "forked_path",
                    Name = "Forked Path",
                    Description = "A crossroads lined with signposts pointing toward nearby settlements.",
                    Biome = "Grassland",
                    ThreatLevel = "Low",
                    AdjacentLocationIds = ["travelers_road", "emberbrook_gate", "mossy_trail", "roadside_inn"]
                },

                new()
                {
                    Id = "roadside_inn",
                    Name = "Dusty Rest Inn",
                    Description = "A roadside tavern where caravans share news over warm stew.",
                    Biome = "Grassland",
                    ThreatLevel = "Safe",
                    AdjacentLocationIds = ["travelers_road", "forked_path"]
                },

                new()
                {
                    Id = "emberbrook_gate",
                    Name = "Emberbrook Gate",
                    Description = "The sturdy wooden gate guarding the village of Emberbrook.",
                    Biome = "Village",
                    ThreatLevel = "Low",
                    AdjacentLocationIds = ["forked_path", "emberbrook_square"],
                    TownName = "Emberbrook"
                },

                new()
                {
                    Id = "emberbrook_square",
                    Name = "Emberbrook Square",
                    Description = "A bustling square where villagers trade stories and goods.",
                    Biome = "Village",
                    ThreatLevel = "Safe",
                    AdjacentLocationIds = ["emberbrook_gate", "emberbrook_inn"],
                    TownName = "Emberbrook"
                },

                new()
                {
                    Id = "emberbrook_inn",
                    Name = "Emberbrook Hearth Inn",
                    Description = "A cozy inn with a glowing hearth and plenty of gossip.",
                    Biome = "Village",
                    ThreatLevel = "Safe",
                    AdjacentLocationIds = ["emberbrook_square"],
                    TownName = "Emberbrook"
                },

                new()
                {
                    Id = "mossy_trail",
                    Name = "Mossy Trail",
                    Description = "A damp trail leading deeper into the trees with soft moss underfoot.",
                    Biome = "Grassland",
                    ThreatLevel = "Low",
                    AdjacentLocationIds = ["forked_path", "whispering_woods"]
                },

                new()
                {
                    Id = "whispering_woods",
                    Name = "Whispering Woods",
                    Description = "The wind carries faint whispers through the dense canopy.",
                    Biome = "Forest",
                    ThreatLevel = "Moderate",
                    AdjacentLocationIds = ["mossy_trail", "mosslight_green", "hollow_cave"]
                },

                new()
                {
                    Id = "mosslight_green",
                    Name = "Mosslight Green",
                    Description = "A sun-dappled clearing where the villagers of Mosslight gather.",
                    Biome = "Village",
                    ThreatLevel = "Low",
                    AdjacentLocationIds = ["whispering_woods", "mosslight_market"],
                    TownName = "Mosslight"
                },

                new()
                {
                    Id = "mosslight_market",
                    Name = "Mosslight Market",
                    Description = "Stalls with fragrant herbs and carved trinkets line the path.",
                    Biome = "Village",
                    ThreatLevel = "Safe",
                    AdjacentLocationIds = ["mosslight_green", "sagestone_tavern"],
                    TownName = "Mosslight"
                },

                new()
                {
                    Id = "sagestone_tavern",
                    Name = "Sagestone Tavern",
                    Description = "Travelers swap rumors over mugs etched with old runes.",
                    Biome = "Village",
                    ThreatLevel = "Low",
                    AdjacentLocationIds = ["mosslight_market", "blackmarsh_crossing"]
                },

                new()
                {
                    Id = "hollow_cave",
                    Name = "Hollow Cave",
                    Description = "A yawning cave mouth breathing cool, damp air.",
                    Biome = "Cave",
                    ThreatLevel = "Moderate",
                    AdjacentLocationIds = ["whispering_woods", "sunless_depths"]
                },

                new()
                {
                    Id = "sunless_depths",
                    Name = "Sunless Depths",
                    Description = "The cave descends into a maze of echoing tunnels.",
                    Biome = "Cave",
                    ThreatLevel = "High",
                    AdjacentLocationIds = ["hollow_cave", "broken_keep"]
                },

                new()
                {
                    Id = "broken_keep",
                    Name = "Broken Keep",
                    Description = "Ruined battlements crumble over a forgotten cellar.",
                    Biome = "Ruins",
                    ThreatLevel = "High",
                    AdjacentLocationIds = ["sunless_depths", "blackmarsh_crossing"]
                },

                new()
                {
                    Id = "blackmarsh_crossing",
                    Name = "Blackmarsh Crossing",
                    Description = "Soggy boardwalks stretch over the dark marsh water.",
                    Biome = "Swamp",
                    ThreatLevel = "Moderate",
                    AdjacentLocationIds = ["sagestone_tavern", "broken_keep", "coastal_road"]
                },

                new()
                {
                    Id = "coastal_road",
                    Name = "Coastal Road",
                    Description = "A sea-sprayed road dotted with fisher carts and gulls.",
                    Biome = "Coast",
                    ThreatLevel = "Low",
                    AdjacentLocationIds = ["blackmarsh_crossing", "stormwatch_gate", "old_watchtower"]
                },

                new()
                {
                    Id = "stormwatch_gate",
                    Name = "Stormwatch Gate",
                    Description = "Weathered stone arches lead into the harbor city.",
                    Biome = "Coast",
                    ThreatLevel = "Low",
                    AdjacentLocationIds = ["coastal_road", "stormwatch_market", "stormwatch_docks"],
                    TownName = "Stormwatch Harbor"
                },

                new()
                {
                    Id = "stormwatch_market",
                    Name = "Stormwatch Market",
                    Description = "Vendors call out prices for fresh catches and exotic spices.",
                    Biome = "Coast",
                    ThreatLevel = "Safe",
                    AdjacentLocationIds = ["stormwatch_gate"],
                    TownName = "Stormwatch Harbor"
                },

                new()
                {
                    Id = "stormwatch_docks",
                    Name = "Stormwatch Docks",
                    Description = "Ships bob in the harbor beside nets heavy with fish.",
                    Biome = "Coast",
                    ThreatLevel = "Low",
                    AdjacentLocationIds = ["stormwatch_gate"],
                    TownName = "Stormwatch Harbor"
                },

                new()
                {
                    Id = "old_watchtower",
                    Name = "Old Watchtower",
                    Description = "A leaning tower that still offers a sweeping coastal view.",
                    Biome = "Ruins",
                    ThreatLevel = "Moderate",
                    AdjacentLocationIds = ["coastal_road", "highridge_pass"]
                },

                new()
                {
                    Id = "highridge_pass",
                    Name = "Highridge Pass",
                    Description = "A narrow pass winding upward through jagged cliffs.",
                    Biome = "Mountain",
                    ThreatLevel = "High",
                    AdjacentLocationIds = ["old_watchtower", "highridge_plaza"]
                },

                new()
                {
                    Id = "highridge_plaza",
                    Name = "Highridge Plaza",
                    Description = "The mountain town's central square overlooking the valley.",
                    Biome = "Mountain",
                    ThreatLevel = "Moderate",
                    AdjacentLocationIds = ["highridge_pass", "highridge_forge"],
                    TownName = "Highridge"
                },

                new()
                {
                    Id = "highridge_forge",
                    Name = "Highridge Forge",
                    Description = "Forgefires light the snow around the mountain smithy.",
                    Biome = "Mountain",
                    ThreatLevel = "Moderate",
                    AdjacentLocationIds = ["highridge_plaza"],
                    TownName = "Highridge"
                }
            ];
        }

        public static List<DropTable> DropTables()
        {
            return
            [
                new()
                {
                    Biome = "Village",
                    Drops =
                    [
                        "loaf_of_bread", "bundle_of_roots", "healing_herbs", "travel_rations", "minor_healing_potion",
                        "wool_vest"
                    ]
                },

                new()
                {
                    Biome = "Grassland",
                    Drops = ["loaf_of_bread", "coin_pouch", "torn_cloth", "field_beans", "rusted_dagger"]
                },

                new()
                {
                    Biome = "Forest",
                    Drops = ["healing_herbs", "tree_sap", "forest_tokens", "stack_of_fungus", "oak_bow"]
                },

                new()
                {
                    Biome = "Mountain",
                    Drops = ["ore_fragment", "sturdy_leather", "coin_pouch", "glowing_crystal", "iron_ingot", "iron_shield"]
                },

                new()
                {
                    Biome = "Cave",
                    Drops = ["glowing_crystal", "bat_wing", "ore_fragment", "crystal_flask", "ashen_staff"]
                },

                new()
                {
                    Biome = "Ruins",
                    Drops = ["ancient_coin", "tattered_map", "mysterious_trinket", "chain_hauberk"]
                },

                new()
                {
                    Biome = "Coast",
                    Drops = ["salted_fish", "coin_pouch", "driftwood_charm", "rope_coil", "sea_blade", "smuggler_cutlass"]
                },

                new()
                {
                    Biome = "Swamp",
                    Drops = ["reedy_bundle", "marsh_pearl", "mossy_fragment", "marsh_hood", "bog_spice"]
                },

                new()
                {
                    Biome = "Unknown",
                    Drops = ["coin_pouch", "mysterious_trinket", "tattered_map", "lesser_mana_potion"]
                }
            ];
        }
    }
}
