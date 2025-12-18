using Domain.Database;
using Domain.Entities.Armor.Models;

namespace Domain.Repos.Armor;
public class HelmetRepository : BaseRepo<HelmetEntity>, IHelmetRepository
{
    public HelmetRepository(IGameDatabase database) : base(database, db => db.Helmets)
    {
    }
}
