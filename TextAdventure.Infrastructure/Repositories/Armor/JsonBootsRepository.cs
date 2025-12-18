using Domain.Database;
using Domain.Entities.Armor.Models;
using Domain.Repos.Armor;
using Domain.Repos;

namespace TextAdventure.Infrastructure.Repositories.Armor;

public class JsonBootsRepository : BaseRepo<BootsEntity>, IBootsRepository
{
    public JsonBootsRepository(IGameDatabase database) : base(database, db => db.Boots)
    {
    }
}
