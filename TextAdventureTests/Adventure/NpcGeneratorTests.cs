using ApplicationServices.Adventure;
using ApplicationServices.Contracts.Services;
using Shouldly;

namespace TextAdventureTests.Adventure;

public class NpcGeneratorTests
{
    [Fact]
    public void GenerateTownNpcs_ReturnsNamedRolesWithVendorMix()
    {
        var generator = new NpcGenerator(new FixedRandomService());

        var npcs = generator.GenerateTownNpcs("Harbor", 5).ToList();

        npcs.Count.ShouldBe(5);
        npcs.ShouldAllBe(n => !string.IsNullOrWhiteSpace(n.Name));
        npcs.ShouldAllBe(n => !string.IsNullOrWhiteSpace(n.Role));
        npcs.ShouldContain(n => n.IsVendor);
    }

    private sealed class FixedRandomService : IRandomService
    {
        private int _counter;

        public int NextInt(int minInclusive, int maxExclusive)
        {
            _counter = (_counter + 1) % (maxExclusive - minInclusive);
            return minInclusive + _counter;
        }

        public double NextDouble() => 0.15;

        public byte[] GetBytes(int length) => Enumerable.Repeat((byte)2, length).ToArray();
    }
}
