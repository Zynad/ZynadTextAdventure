using Domain.Entities.Items.Models;
using Domain.Enums;

namespace Domain.Database;

public static partial class DbCreateDefault
{
    public static partial class Database
    {
        public static IEnumerable<GenericItemEntity> Items()
        {
            return new List<GenericItemEntity>
            {
                new()
                {
                    Id = Guid.Parse("1c9c9d59-1f2c-4b1e-86b1-6f53a0baf001"),
                    Name = "Travel Rations",
                    LevelRequirement = 1,
                    Rarity = RarityEntity.Common,
                    Value = 5,
                    Weight = 1
                },
                new()
                {
                    Id = Guid.Parse("2f4a51a7-2532-4e55-8f34-621bb5c1f002"),
                    Name = "Bundle of Roots",
                    LevelRequirement = 1,
                    Rarity = RarityEntity.Common,
                    Value = 3,
                    Weight = 1
                },
                new()
                {
                    Id = Guid.Parse("38dd21e1-3a2a-4e6f-8bfa-9c4b9b8d3003"),
                    Name = "Loaf of Bread",
                    LevelRequirement = 1,
                    Rarity = RarityEntity.Common,
                    Value = 2,
                    Weight = 1
                },
                new()
                {
                    Id = Guid.Parse("4a2b12e7-5c32-4219-9881-9af5d5b74004"),
                    Name = "Minor Healing Potion",
                    LevelRequirement = 2,
                    Rarity = RarityEntity.Common,
                    Value = 15,
                    Weight = 1
                },
                new()
                {
                    Id = Guid.Parse("55f6b48c-01f1-4b46-9e6b-c3b704f84005"),
                    Name = "Antidote Phial",
                    LevelRequirement = 2,
                    Rarity = RarityEntity.Uncommon,
                    Value = 18,
                    Weight = 1
                },
                new()
                {
                    Id = Guid.Parse("6bd9588f-9c46-4a18-9c08-f3d969d12006"),
                    Name = "Quiver of Arrows",
                    LevelRequirement = 2,
                    Rarity = RarityEntity.Common,
                    Value = 14,
                    Weight = 2
                },
                new()
                {
                    Id = Guid.Parse("75197ad1-6b2d-46eb-98d8-5e47e4b32007"),
                    Name = "Whetstone",
                    LevelRequirement = 1,
                    Rarity = RarityEntity.Common,
                    Value = 8,
                    Weight = 1
                },
                new()
                {
                    Id = Guid.Parse("8722da12-741d-4b75-83a5-120de6cfe008"),
                    Name = "Rope Coil",
                    LevelRequirement = 1,
                    Rarity = RarityEntity.Common,
                    Value = 7,
                    Weight = 2
                },
                new()
                {
                    Id = Guid.Parse("993195f1-dc5c-4f54-8fa9-cb0de2af6009"),
                    Name = "Iron Ingot",
                    LevelRequirement = 3,
                    Rarity = RarityEntity.Uncommon,
                    Value = 22,
                    Weight = 4
                },
                new()
                {
                    Id = Guid.Parse("a7cf0862-5c7e-4d46-a2b6-0c5c3cf57010"),
                    Name = "Glowing Crystal",
                    LevelRequirement = 4,
                    Rarity = RarityEntity.Uncommon,
                    Value = 28,
                    Weight = 2
                },
                new()
                {
                    Id = Guid.Parse("b1a69f62-329b-4f10-9558-6ed9988b3011"),
                    Name = "Herbal Tonic",
                    LevelRequirement = 2,
                    Rarity = RarityEntity.Common,
                    Value = 10,
                    Weight = 1
                },
                new()
                {
                    Id = Guid.Parse("c3e4f6ae-2a63-4a1a-b22a-1f4c6915b012"),
                    Name = "Leather Cap",
                    LevelRequirement = 1,
                    Rarity = RarityEntity.Common,
                    Value = 12,
                    Weight = 1
                },
                new()
                {
                    Id = Guid.Parse("d590b3bc-07a1-4c50-8e7e-3b9a8f5d7013"),
                    Name = "Sturdy Leather",
                    LevelRequirement = 2,
                    Rarity = RarityEntity.Common,
                    Value = 9,
                    Weight = 2
                },
                new()
                {
                    Id = Guid.Parse("e7d3415c-9c1d-46e3-9a8b-60d0b0b8c014"),
                    Name = "Healer's Poultice",
                    LevelRequirement = 3,
                    Rarity = RarityEntity.Uncommon,
                    Value = 20,
                    Weight = 1
                },
                new()
                {
                    Id = Guid.Parse("f97d9c2b-7f5d-4f9a-9e4c-91561d115015"),
                    Name = "Lesser Mana Potion",
                    LevelRequirement = 3,
                    Rarity = RarityEntity.Common,
                    Value = 16,
                    Weight = 1
                },
                new()
                {
                    Id = Guid.Parse("0ac0f6c2-9b3c-4b3b-9bc9-541a42edb016"),
                    Name = "Iron Shield",
                    LevelRequirement = 3,
                    Rarity = RarityEntity.Uncommon,
                    Value = 28,
                    Weight = 6
                },
                new()
                {
                    Id = Guid.Parse("1b2d3f44-d0a6-4cde-baf0-34d7fb4c7017"),
                    Name = "Salted Fish",
                    LevelRequirement = 1,
                    Rarity = RarityEntity.Common,
                    Value = 4,
                    Weight = 1
                },
                new()
                {
                    Id = Guid.Parse("2cd4130f-6c9f-4475-9383-7090c6d57018"),
                    Name = "Driftwood Charm",
                    LevelRequirement = 2,
                    Rarity = RarityEntity.Common,
                    Value = 9,
                    Weight = 1
                },
                new()
                {
                    Id = Guid.Parse("37c06f11-06ee-4ce3-8c1d-170e0413c019"),
                    Name = "Rugged Cloak",
                    LevelRequirement = 2,
                    Rarity = RarityEntity.Common,
                    Value = 11,
                    Weight = 2
                },
                new()
                {
                    Id = Guid.Parse("4e8db1af-4c4c-4701-a7d0-3b5e6fcbb020"),
                    Name = "Sailor's Gloves",
                    LevelRequirement = 2,
                    Rarity = RarityEntity.Common,
                    Value = 8,
                    Weight = 1
                },
                new()
                {
                    Id = Guid.Parse("5f2a6d32-efde-4275-9641-4d2d6bb53021"),
                    Name = "Sage's Quill",
                    LevelRequirement = 1,
                    Rarity = RarityEntity.Common,
                    Value = 6,
                    Weight = 1
                },
                new()
                {
                    Id = Guid.Parse("6b5f9437-1db3-4bdd-971f-7c978e7ad022"),
                    Name = "Copper Wire Coil",
                    LevelRequirement = 2,
                    Rarity = RarityEntity.Common,
                    Value = 7,
                    Weight = 1
                },
                new()
                {
                    Id = Guid.Parse("7cdf5e32-d9a4-4f78-9b44-48004e3f4023"),
                    Name = "Torch Bundle",
                    LevelRequirement = 1,
                    Rarity = RarityEntity.Common,
                    Value = 5,
                    Weight = 2
                },
                new()
                {
                    Id = Guid.Parse("8e14d303-1795-4f55-a0ba-48377b1de024"),
                    Name = "Surveyor's Map",
                    LevelRequirement = 2,
                    Rarity = RarityEntity.Uncommon,
                    Value = 14,
                    Weight = 1
                },
                new()
                {
                    Id = Guid.Parse("9f2c9c24-9493-41a8-9a8f-8a2d74123025"),
                    Name = "Reinforced Pack",
                    LevelRequirement = 3,
                    Rarity = RarityEntity.Uncommon,
                    Value = 18,
                    Weight = 2
                },
                new()
                {
                    Id = Guid.Parse("ae10f80f-6c1f-4a93-a21d-51d72a566026"),
                    Name = "Traveler's Compass",
                    LevelRequirement = 2,
                    Rarity = RarityEntity.Uncommon,
                    Value = 16,
                    Weight = 1
                },
                new()
                {
                    Id = Guid.Parse("bf2406e9-47dd-4cc6-9d30-7bf8695f5027"),
                    Name = "Herbal Satchel",
                    LevelRequirement = 2,
                    Rarity = RarityEntity.Common,
                    Value = 9,
                    Weight = 1
                },
                new()
                {
                    Id = Guid.Parse("cdae6b2b-7b37-4a97-91a0-6301d8d2e028"),
                    Name = "Wayfinder's Lens",
                    LevelRequirement = 4,
                    Rarity = RarityEntity.Rare,
                    Value = 35,
                    Weight = 1
                },
                new()
                {
                    Id = Guid.Parse("da7d3f2e-2c7d-469a-9477-fb8f93e69029"),
                    Name = "Adventurer's Toolkit",
                    LevelRequirement = 3,
                    Rarity = RarityEntity.Uncommon,
                    Value = 24,
                    Weight = 3
                },
                new()
                {
                    Id = Guid.Parse("ebb8cf58-9294-40d6-96b3-145cfa9fa030"),
                    Name = "Stonemason's Chisel",
                    LevelRequirement = 2,
                    Rarity = RarityEntity.Common,
                    Value = 10,
                    Weight = 2
                }
            };
        }
    }
}
