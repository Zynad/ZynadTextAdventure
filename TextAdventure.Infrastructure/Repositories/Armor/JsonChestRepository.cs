using Domain.Database;
using Domain.Entities.Armor.Models;
using Domain.Repos.Armor;
using Domain.Repos;

namespace TextAdventure.Infrastructure.Repositories.Armor;

public class JsonChestRepository(IGameDatabase database)
    : BaseRepo<ChestEntity>(database, db => db.Chests), IChestRepository;
