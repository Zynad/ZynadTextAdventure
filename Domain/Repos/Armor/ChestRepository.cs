using Domain.Database;
using Domain.Entities.Armor.Models;

namespace Domain.Repos.Armor;
public class ChestRepository(IGameDatabase database)
    : BaseRepo<ChestEntity>(database, db => db.Chests), IChestRepository;
