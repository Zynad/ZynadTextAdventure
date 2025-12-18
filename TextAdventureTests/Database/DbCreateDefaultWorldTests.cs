using Domain.Database;
using Shouldly;

namespace TextAdventureTests.Database;

public class DbCreateDefaultWorldTests
{
    [Fact]
    public void Towns_IncludeVendorsAndNpcs()
    {
        var towns = DbCreateDefault.World.Towns();

        towns.ShouldNotBeEmpty();
        foreach (var town in towns)
        {
            town.Npcs.ShouldNotBeEmpty();
            town.VendorInventory.Count.ShouldBeGreaterThanOrEqualTo(10);
        }
    }
}
