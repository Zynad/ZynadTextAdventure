using Domain.Database;
using Domain.Entities.Weapons.Models;
using Domain.Repos.Weapons;

namespace TextAdventure.Infrastructure.Repositories.Weapons;

public class JsonSwordRepository : BaseRepo<SwordEntity>, ISwordRepository
{
    public JsonSwordRepository(IGameDatabase database) : base(database, db => db.Swords)
    {
    }
}
