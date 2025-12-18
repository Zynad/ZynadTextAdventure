using Domain.Database;
using Domain.Entities.Weapons.Models;
using Domain.Repos.Weapons;

namespace TextAdventure.Infrastructure.Repositories.Weapons;

public class JsonAxeRepository : BaseRepo<AxeEntity>, IAxeRepository
{
    public JsonAxeRepository(IGameDatabase database) : base(database, db => db.Axes)
    {
    }
}
