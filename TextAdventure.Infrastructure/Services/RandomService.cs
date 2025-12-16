using System.Security.Cryptography;
using ApplicationServices.Contracts.Services;

namespace TextAdventure.Infrastructure.Services;

public class RandomService : IRandomService
{
    public byte[] GetBytes(int length)
    {
        return RandomNumberGenerator.GetBytes(length);
    }

    public double NextDouble()
    {
        Span<byte> buffer = stackalloc byte[8];
        RandomNumberGenerator.Fill(buffer);
        var ulongValue = BitConverter.ToUInt64(buffer);
        return ulongValue / (double)ulong.MaxValue;
    }

    public int NextInt(int minInclusive, int maxExclusive)
    {
        return RandomNumberGenerator.GetInt32(minInclusive, maxExclusive);
    }
}
