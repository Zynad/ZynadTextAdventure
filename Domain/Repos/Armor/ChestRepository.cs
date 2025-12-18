using Domain.Database;
using Domain.Entities.Armor.Models;

namespace Domain.Repos.Armor;
public class ChestRepository : BaseRepo<ChestEntity>, IChestRepository
{
    public ChestRepository(IGameDatabase database) : base(database, db => db.Chests)
    {
    }
}
