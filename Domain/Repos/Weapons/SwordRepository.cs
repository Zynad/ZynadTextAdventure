using Domain.Database;
using Domain.Entities.Weapons.Models;

namespace Domain.Repos.Weapons;
public class SwordRepository : BaseRepo<SwordEntity>, ISwordRepository
{
    public SwordRepository(IGameDatabase database) : base(database, db => db.Swords)
    {
    }
}
