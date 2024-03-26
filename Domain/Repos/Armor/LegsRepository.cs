using Domain.Contexts;
using Domain.Entities.Armor.Models;

namespace Domain.Repos.Armor;

public class LegsRepository : BaseRepo<LegsEntity>, ILegsRepository
{
    public LegsRepository(DataContext context) : base(context)
    {
    }
}

