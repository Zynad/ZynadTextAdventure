using Domain.Core;
using Domain.ValueObjects;

namespace Domain.Database;

public static partial class DbCreateDefault
{
    public static partial class World
    {
        public static List<Quest> Quests()
        {
            return new List<Quest>
            {
                new()
                {
                    Id = "emberbrook_supplies",
                    Name = "Restock the Storehouse",
                    Description = "Gather travel rations and herbs to help Emberbrook restock before the next caravan arrives.",
                    TownName = "Emberbrook",
                    AcceptLocationId = "emberbrook_mayor",
                    CompletionLocationId = "emberbrook_mayor",
                    RewardItems =
                    [
                        new InventoryItem { ItemId = "travel_rations", Quantity = 4 },
                        new InventoryItem { ItemId = "minor_healing_potion", Quantity = 1 }
                    ]
                },
                new()
                {
                    Id = "mosslight_watch",
                    Name = "Guard the Western Path",
                    Description = "Assist Ser Havel in checking the western approach for bandits and report back.",
                    TownName = "Mosslight",
                    AcceptLocationId = "mosslight_guard",
                    CompletionLocationId = "mosslight_guard",
                    RewardItems =
                    [
                        new InventoryItem { ItemId = "antidote_phial", Quantity = 2 },
                        new InventoryItem { ItemId = "quiver_of_arrows", Quantity = 1 }
                    ]
                },
                new()
                {
                    Id = "stormwatch_manifest",
                    Name = "Tally the Manifest",
                    Description = "Help Dockmaster Leira reconcile the pier manifest and recover missing goods.",
                    TownName = "Stormwatch Harbor",
                    AcceptLocationId = "stormwatch_dockmaster",
                    CompletionLocationId = "stormwatch_dockmaster",
                    RewardItems =
                    [
                        new InventoryItem { ItemId = "driftwood_charm", Quantity = 1 },
                        new InventoryItem { ItemId = "rope_coil", Quantity = 2 }
                    ]
                },
                new()
                {
                    Id = "highridge_tools",
                    Name = "Tools for the Depths",
                    Description = "Deliver fresh tools and provisions to the miners working the lower tunnel in Highridge.",
                    TownName = "Highridge",
                    AcceptLocationId = "highridge_quartermaster",
                    CompletionLocationId = "highridge_miner",
                    RewardItems =
                    [
                        new InventoryItem { ItemId = "iron_ingot", Quantity = 2 },
                        new InventoryItem { ItemId = "sturdy_leather", Quantity = 2 }
                    ],
                    PrerequisiteQuestIds = ["mosslight_watch"]
                },
                new()
                {
                    Id = "wanderers_map",
                    Name = "Map the Wilds",
                    Description = "Chart safe paths between Mosslight and Highridge for traveling caravans.",
                    TownName = "Mosslight",
                    AcceptLocationId = "mosslight_scavenger",
                    CompletionLocationId = "highridge_quartermaster",
                    RewardItems =
                    [
                        new InventoryItem { ItemId = "surveyor_map", Quantity = 1 },
                        new InventoryItem { ItemId = "reinforced_pack", Quantity = 1 }
                    ],
                    PrerequisiteQuestIds = ["emberbrook_supplies"]
                }
            };
        }
    }
}
