using Domain.Contexts;
using Domain.Entities.Weapons.Models;

namespace Domain.Repos.Weapons;

public class SwordRepository : BaseRepo<SwordEntity>, ISwordRepository
{
    public SwordRepository(DataContext context) : base(context)
    {
    }
}

