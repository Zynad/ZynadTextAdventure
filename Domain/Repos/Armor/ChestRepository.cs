using Domain.Contexts;
using Domain.Entities.Armor.Models;

namespace Domain.Repos.Armor;

public class ChestRepository : BaseRepo<ChestEntity>, IChestRepository
{
    public ChestRepository(DataContext context) : base(context)
    {
    }
}

