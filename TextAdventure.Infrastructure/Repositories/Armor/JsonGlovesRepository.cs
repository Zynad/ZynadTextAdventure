using Domain.Database;
using Domain.Entities.Armor.Models;
using Domain.Repos.Armor;

namespace TextAdventure.Infrastructure.Repositories.Armor;

public class JsonGlovesRepository : BaseRepo<GlovesEntity>, IGlovesRepository
{
    public JsonGlovesRepository(IGameDatabase database) : base(database, db => db.Gloves)
    {
    }
}
