using Domain.Database;
using Domain.Entities.Weapons.Models;

namespace Domain.Repos.Weapons;
public class AxeRepository : BaseRepo<AxeEntity>, IAxeRepository
{
    public AxeRepository(IGameDatabase database) : base(database, db => db.Axes)
    {
    }
}
