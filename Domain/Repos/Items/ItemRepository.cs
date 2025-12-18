using Domain.Database;
using Domain.Entities.Items.Models;

namespace Domain.Repos.Items;

public class ItemRepository(IGameDatabase database)
    : BaseRepo<GenericItemEntity>(database, db => db.Items), IItemRepository;
