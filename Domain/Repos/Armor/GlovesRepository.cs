using Domain.Database;
using Domain.Entities.Armor.Models;

namespace Domain.Repos.Armor;
public class GlovesRepository : BaseRepo<GlovesEntity>, IGlovesRepository
{
    public GlovesRepository(IGameDatabase database) : base(database, db => db.Gloves)
    {
    }
}
