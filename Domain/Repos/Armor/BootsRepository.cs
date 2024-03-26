using Domain.Contexts;
using Domain.Entities.Armor.Models;

namespace Domain.Repos.Armor;

public class BootsRepository : BaseRepo<BootsEntity>, IBootsRepository
{
    public BootsRepository(DataContext context) : base(context)
    {
    }
}

