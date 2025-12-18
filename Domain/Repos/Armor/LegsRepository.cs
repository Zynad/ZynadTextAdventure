using Domain.Database;
using Domain.Entities.Armor.Models;

namespace Domain.Repos.Armor;
public class LegsRepository : BaseRepo<LegsEntity>, ILegsRepository
{
    public LegsRepository(IGameDatabase database) : base(database, db => db.Legs)
    {
    }
}
