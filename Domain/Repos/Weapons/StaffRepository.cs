using Domain.Contexts;
using Domain.Entities.Weapons.Models;

namespace Domain.Repos.Weapons;

public class StaffRepository : BaseRepo<StaffEntity>, IStaffRepository
{
    public StaffRepository(DataContext context) : base(context)
    {
    }
}

