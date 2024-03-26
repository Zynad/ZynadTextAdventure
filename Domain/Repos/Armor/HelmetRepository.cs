using Domain.Contexts;
using Domain.Entities.Armor.Models;

namespace Domain.Repos.Armor;

public class HelmetRepository : BaseRepo<HelmetEntity>, IHelmetRepository
{
    public HelmetRepository(DataContext context) : base(context)
    {
    }
}

