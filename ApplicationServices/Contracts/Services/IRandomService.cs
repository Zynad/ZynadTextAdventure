namespace ApplicationServices.Contracts.Services;

public interface IRandomService
{
    int NextInt(int minInclusive, int maxExclusive);
    double NextDouble();
    byte[] GetBytes(int length);
}
