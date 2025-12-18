using Domain.Database;
using Domain.Entities.Weapons.Models;

namespace Domain.Repos.Weapons;
public class WandRepository : BaseRepo<WandEntity>, IWandRepository
{
    public WandRepository(IGameDatabase database) : base(database, db => db.Wands)
    {
    }
}
