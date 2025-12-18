using Domain.Database;
using Domain.Entities.Items.Models;

namespace Domain.Repos.Items;

public class ItemRepository : BaseRepo<GenericItemEntity>, IItemRepository
{
    public ItemRepository(IGameDatabase database)
        : base(database, db => db.Items)
    {
    }
}
