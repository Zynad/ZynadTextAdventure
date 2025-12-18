using Domain.Database;
using Domain.Entities.Weapons.Models;

namespace Domain.Repos.Weapons;
public class StaffRepository : BaseRepo<StaffEntity>, IStaffRepository
{
    public StaffRepository(IGameDatabase database) : base(database, db => db.Staff)
    {
    }
}
