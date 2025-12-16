using ApplicationServices.Contracts.Services;
using Domain.Core;

namespace ApplicationServices.Adventure;

public class NpcGenerator
{
    private readonly IRandomService _random;

    private static readonly string[] GivenNames =
    [
        "Alda", "Brin", "Caro", "Dessa", "Eldric", "Fenna", "Garr", "Hale", "Isen", "Joren",
        "Kessa", "Lir", "Merrin", "Nia", "Oren", "Pella", "Quill", "Rhea", "Serra", "Torun"
    ];

    private static readonly string[] Surnames =
    [
        "Ashgrove", "Brighton", "Coppervein", "Dawnfield", "Eldoak", "Frost", "Gale", "Hearth",
        "Ironweaver", "Juniper", "Keene", "Lowell", "Moss", "North", "Oakhand", "Pike", "Quarry",
        "Reed", "Slate", "Tanner"
    ];

    private static readonly string[] Roles =
    [
        "Wanderer", "Herbalist", "Scribe", "Hunter", "Trader", "Blacksmith", "Caravaneer", "Sailor",
        "Guard", "Storyteller", "Musician", "Mason"
    ];

    public NpcGenerator(IRandomService random)
    {
        _random = random;
    }

    public IReadOnlyCollection<TownNpc> GenerateTownNpcs(string townName, int count, bool allowVendors = true)
    {
        var generated = new List<TownNpc>(count);
        for (var i = 0; i < count; i++)
        {
            var given = GivenNames[_random.NextInt(0, GivenNames.Length)];
            var surname = Surnames[_random.NextInt(0, Surnames.Length)];
            var role = Roles[_random.NextInt(0, Roles.Length)];
            var isVendor = allowVendors && _random.NextDouble() < 0.2;

            generated.Add(new TownNpc
            {
                Id = $"npc_{townName.ToLowerInvariant()}_{i}",
                Name = $"{given} {surname}",
                Role = role,
                Personality = _random.NextDouble() < 0.5 ? "Amiable" : "Reserved",
                IsVendor = isVendor
            });
        }

        return generated;
    }
}
