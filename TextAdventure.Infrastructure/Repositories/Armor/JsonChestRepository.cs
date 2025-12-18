using Domain.Database;
using Domain.Entities.Armor.Models;
using Domain.Repos.Armor;
using Domain.Repos;

namespace TextAdventure.Infrastructure.Repositories.Armor;

public class JsonChestRepository : BaseRepo<ChestEntity>, IChestRepository
{
    public JsonChestRepository(IGameDatabase database) : base(database, db => db.Chests)
    {
    }
}
