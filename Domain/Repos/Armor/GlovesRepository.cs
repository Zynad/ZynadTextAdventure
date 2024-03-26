using Domain.Contexts;
using Domain.Entities.Armor.Models;

namespace Domain.Repos.Armor;

public class GlovesRepository : BaseRepo<GlovesEntity>, IGlovesRepository
{
    public GlovesRepository(DataContext context) : base(context)
    {
    }
}

