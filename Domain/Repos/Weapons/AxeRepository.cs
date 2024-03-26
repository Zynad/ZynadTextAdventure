using Domain.Contexts;
using Domain.Entities.Weapons.Models;

namespace Domain.Repos.Weapons;

public class AxeRepository : BaseRepo<AxeEntity>, IAxeRepository
{
    public AxeRepository(DataContext context) : base(context)
    {
    }
}

