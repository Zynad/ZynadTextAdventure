using Domain.Database;
using Domain.Entities.Weapons.Models;
using Domain.Repos.Weapons;

namespace TextAdventure.Infrastructure.Repositories.Weapons;

public class JsonWandRepository : BaseRepo<WandEntity>, IWandRepository
{
    public JsonWandRepository(IGameDatabase database) : base(database, db => db.Wands)
    {
    }
}
