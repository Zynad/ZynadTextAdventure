using Domain.Core;

namespace Domain.Database;

public static partial class DbCreateDefault
{
    public static partial class World
    {
        public static List<Town> Towns()
        {
            return
            [
                new()
                {
                    Name = "Emberbrook",
                    VendorInventory =
                    [
                        new() { ItemId = "loaf_of_bread", BuyPrice = 2.0m, SellPrice = 1.0m },
                        new() { ItemId = "whetstone", BuyPrice = 8.0m, SellPrice = 3.0m },
                        new() { ItemId = "leather_cap", BuyPrice = 12.0m, SellPrice = 5.0m },
                        new() { ItemId = "healing_herbs", BuyPrice = 6.0m, SellPrice = 3.0m },
                        new() { ItemId = "minor_healing_potion", BuyPrice = 14.0m, SellPrice = 6.0m },
                        new() { ItemId = "bundle_of_roots", BuyPrice = 4.0m, SellPrice = 2.0m },
                        new() { ItemId = "travel_rations", BuyPrice = 5.0m, SellPrice = 2.0m },
                        new() { ItemId = "torch_bundle", BuyPrice = 5.0m, SellPrice = 2.0m },
                        new() { ItemId = "herbal_tonic", BuyPrice = 10.0m, SellPrice = 4.0m },
                        new() { ItemId = "herbal_satchel", BuyPrice = 12.0m, SellPrice = 5.0m }
                    ],
                    Npcs =
                    [
                        BuildNpc("Emberbrook", "emberbrook_mayor", "Mayor Thale", "Mayor", "Earnest",
                            roleType: NpcRoleType.QuestGiver),

                        BuildNpc("Emberbrook", "emberbrook_farmer", "Rhea Grainley", "Farmer", "Cheerful"),
                        BuildNpc("Emberbrook", "emberbrook_barkeep", "Joren Kask", "Barkeep", "Wry", true)
                    ]
                },

                new()
                {
                    Name = "Mosslight",
                    VendorInventory =
                    [
                        new() { ItemId = "forest_tokens", BuyPrice = 5.0m, SellPrice = 2.0m },
                        new() { ItemId = "travel_rations", BuyPrice = 3.5m, SellPrice = 1.5m },
                        new() { ItemId = "quiver_of_arrows", BuyPrice = 14.0m, SellPrice = 6.0m },
                        new() { ItemId = "healing_herbs", BuyPrice = 6.0m, SellPrice = 3.0m },
                        new() { ItemId = "antidote_phial", BuyPrice = 10.0m, SellPrice = 4.0m },
                        new() { ItemId = "sturdy_leather", BuyPrice = 9.0m, SellPrice = 4.0m },
                        new() { ItemId = "healers_poultice", BuyPrice = 12.0m, SellPrice = 5.0m },
                        new() { ItemId = "surveyor_map", BuyPrice = 14.0m, SellPrice = 6.0m },
                        new() { ItemId = "wayfinder_lens", BuyPrice = 28.0m, SellPrice = 12.0m },
                        new() { ItemId = "adventurer_toolkit", BuyPrice = 24.0m, SellPrice = 10.0m }
                    ],
                    Npcs =
                    [
                        BuildNpc("Mosslight", "mosslight_guard", "Ser Havel", "Guard Captain", "Stoic",
                            roleType: NpcRoleType.Guard),

                        BuildNpc("Mosslight", "mosslight_scavenger", "Fenna Willow", "Forager", "Curious",
                            roleType: NpcRoleType.QuestGiver),

                        BuildNpc("Mosslight", "mosslight_caller", "Brin Bell", "Town Crier", "Booming")
                    ]
                },

                new()
                {
                    Name = "Stormwatch Harbor",
                    VendorInventory =
                    [
                        new() { ItemId = "salted_fish", BuyPrice = 4.0m, SellPrice = 2.0m },
                        new() { ItemId = "driftwood_charm", BuyPrice = 9.0m, SellPrice = 4.0m },
                        new() { ItemId = "rope_coil", BuyPrice = 7.0m, SellPrice = 3.0m },
                        new() { ItemId = "sailor_cloak", BuyPrice = 15.0m, SellPrice = 7.0m },
                        new() { ItemId = "lesser_mana_potion", BuyPrice = 16.0m, SellPrice = 7.0m },
                        new() { ItemId = "sailor_gloves", BuyPrice = 8.0m, SellPrice = 3.0m },
                        new() { ItemId = "rugged_cloak", BuyPrice = 11.0m, SellPrice = 5.0m },
                        new() { ItemId = "traveler_compass", BuyPrice = 16.0m, SellPrice = 7.0m },
                        new() { ItemId = "surveyor_map", BuyPrice = 16.0m, SellPrice = 7.0m },
                        new() { ItemId = "torch_bundle", BuyPrice = 5.0m, SellPrice = 2.0m }
                    ],
                    Npcs =
                    [
                        BuildNpc("Stormwatch Harbor", "stormwatch_dockmaster", "Dockmaster Leira", "Dockmaster", "Gruff",
                            true, NpcRoleType.QuestGiver),

                        BuildNpc("Stormwatch Harbor", "stormwatch_sailor", "Old Wens", "Sailor", "Storyteller"),
                        BuildNpc("Stormwatch Harbor", "stormwatch_scrim", "Scrim", "Smuggler", "Cagey",
                            roleType: NpcRoleType.Flavor)
                    ]
                },

                new()
                {
                    Name = "Highridge",
                    VendorInventory =
                    [
                        new() { ItemId = "ore_fragment", BuyPrice = 10.0m, SellPrice = 4.0m },
                        new() { ItemId = "sturdy_leather", BuyPrice = 9.0m, SellPrice = 4.0m },
                        new() { ItemId = "iron_ingot", BuyPrice = 18.0m, SellPrice = 8.0m },
                        new() { ItemId = "glowing_crystal", BuyPrice = 22.0m, SellPrice = 10.0m },
                        new() { ItemId = "iron_shield", BuyPrice = 28.0m, SellPrice = 12.0m },
                        new() { ItemId = "stonemason_chisel", BuyPrice = 10.0m, SellPrice = 4.0m },
                        new() { ItemId = "copper_wire_coil", BuyPrice = 7.0m, SellPrice = 3.0m },
                        new() { ItemId = "herbal_tonic", BuyPrice = 10.0m, SellPrice = 4.0m },
                        new() { ItemId = "reinforced_pack", BuyPrice = 18.0m, SellPrice = 8.0m },
                        new() { ItemId = "adventurer_toolkit", BuyPrice = 24.0m, SellPrice = 10.0m }
                    ],
                    Npcs =
                    [
                        BuildNpc("Highridge", "highridge_miner", "Torun Slate", "Miner", "Pragmatic"),
                        BuildNpc("Highridge", "highridge_cook", "Elya Pike", "Cook", "Warm"),
                        BuildNpc("Highridge", "highridge_quartermaster", "Quartermaster Hale", "Quartermaster", "Exacting",
                            true, NpcRoleType.Vendor)
                    ]
                }
            ];
        }

        public static TownNpc BuildNpc(string townName, string id, string name, string role, string personality, bool isVendor = false, NpcRoleType roleType = NpcRoleType.Flavor)
        {
            var type = roleType;
            if (type == NpcRoleType.Flavor)
            {
                if (isVendor)
                {
                    type = NpcRoleType.Vendor;
                }
                else if (role.Contains("guard", StringComparison.OrdinalIgnoreCase) || role.Contains("warden", StringComparison.OrdinalIgnoreCase))
                {
                    type = NpcRoleType.Guard;
                }
            }

            return new TownNpc
            {
                Id = id,
                Name = name,
                Role = role,
                Personality = personality,
                IsVendor = isVendor,
                RoleType = type,
                Location = townName,
                QuestsOffered = [$"{id}_rumor"],
                Dialogue = new NpcDialogueTemplate
                {
                    Greetings =
                    [
                        $"Greetings, {"{playerName}"}. I'm {name} of {townName}.",
                        $"{townName} welcomes you, {"{playerName}"}."
                    ],
                    QuestOffers =
                    [
                        $"If you're brave, {"{playerName}"}, I could use help with a matter in {townName}.",
                        $"Spare a moment? {townName} has a task for capable hands."
                    ],
                    Farewells =
                    [
                        "Stay safe out there.",
                        "May your path be clear."
                    ],
                    RandomLines =
                    [
                        $"Have you heard the news from {townName}?",
                        "The roads grow stranger each night."
                    ],
                    TradeOpeners =
                    [
                        "Take a look at my wares.",
                        "Fair prices for a fellow traveler."
                    ]
                }
            };
        }
    }
}
