using Domain.Database;
using Domain.Entities.Armor.Models;
using Domain.Repos.Armor;
using Domain.Repos;

namespace TextAdventure.Infrastructure.Repositories.Armor;

public class JsonHelmetRepository : BaseRepo<HelmetEntity>, IHelmetRepository
{
    public JsonHelmetRepository(IGameDatabase database) : base(database, db => db.Helmets)
    {
    }
}
