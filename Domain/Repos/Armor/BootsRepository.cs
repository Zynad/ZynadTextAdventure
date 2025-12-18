using Domain.Database;
using Domain.Entities.Armor.Models;

namespace Domain.Repos.Armor;
public class BootsRepository : BaseRepo<BootsEntity>, IBootsRepository
{
    public BootsRepository(IGameDatabase database) : base(database, db => db.Boots)
    {
    }
}
