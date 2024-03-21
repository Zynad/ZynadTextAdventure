using Domain.Contexts;
using Domain.Entities.Weapons.Models;

namespace Domain.Repos.Weapons;

public class WandRepository : BaseRepo<WandEntity>, IWandRepository
{
    public WandRepository(DataContext context) : base(context)
    {
    }
}

