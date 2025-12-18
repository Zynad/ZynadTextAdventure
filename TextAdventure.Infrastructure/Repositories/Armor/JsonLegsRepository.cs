using Domain.Database;
using Domain.Entities.Armor.Models;
using Domain.Repos.Armor;

namespace TextAdventure.Infrastructure.Repositories.Armor;

public class JsonLegsRepository : BaseRepo<LegsEntity>, ILegsRepository
{
    public JsonLegsRepository(IGameDatabase database) : base(database, db => db.Legs)
    {
    }
}
