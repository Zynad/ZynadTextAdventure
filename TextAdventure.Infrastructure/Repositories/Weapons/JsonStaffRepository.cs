using Domain.Database;
using Domain.Entities.Weapons.Models;
using Domain.Repos.Weapons;

namespace TextAdventure.Infrastructure.Repositories.Weapons;

public class JsonStaffRepository : BaseRepo<StaffEntity>, IStaffRepository
{
    public JsonStaffRepository(IGameDatabase database) : base(database, db => db.Staff)
    {
    }
}
