using TextAdventure.Items.Equipment.Weapons;
using TextAdventure.Repos.Weapons;

public class WeaponService<T> : IWeaponService<T> where T : WeaponBase
{
    private readonly IWeaponRepository<T> _repo;
    public WeaponService(IWeaponRepository<T> repo)
    {
        _repo = repo;
    }
    public async Task<List<T>> GetWeapons()
    {
        return await _repo.GetWeapons();
    }
    public async Task<T> GetWeapon(Func<T, bool> predicate)
    {
        return await _repo.GetWeapon(predicate);
    }
}